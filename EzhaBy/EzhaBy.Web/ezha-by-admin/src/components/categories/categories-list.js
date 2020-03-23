import React, { Component } from 'react';
import { Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import TagsService from '../../services/tags-service';
import BootstrapTable from 'react-bootstrap-table-next';
import cellEditFactory from 'react-bootstrap-table2-editor';
import ToolkitProvider, { Search } from 'react-bootstrap-table2-toolkit';

const { SearchBar, ClearSearchButton } = Search;

export default class CategoriesList extends Component {
  onDeleteCategory(tagId) {
    //TagsService.deleteTag(tagId);
  }

  onAfterSaveCategory(oldValue, newValue, row, column) {
    //TagsService.updateTagName(row.id, newValue);
  }

  renderRemoveButton(category) {
    return category.isAssigned ? (
      <OverlayTrigger
        key="right"
        placement="right"
        overlay={<Tooltip id={`tooltip-right`}>Тэг назначен заведению</Tooltip>}
      >
        <span>
          <Button
            variant="outline-danger"
            disabled
            style={{ pointerEvents: 'none' }}
          >
            Удалить
          </Button>
        </span>
      </OverlayTrigger>
    ) : (
      <Button onClick={() => this.onDeleteCategory(category.id)} variant="outline-danger">
        Удалить
      </Button>
    );
  }

  render() {
    const columns = [
      {
        dataField: 'id',
        text: 'ID',
        hidden: true
      },
      {
        dataField: 'categoryName',
        text: 'Категории',
        align: 'left',
        headerAlign: 'left',
        sort: true,
        validator: (newValue, row, column) => {
          if (newValue.length === 0) {
            return {
              valid: false,
              message: 'Введите название'
            };
          }
          return true;
        }
      },
      {
        dataField: 'remove',
        isDummyField: true,
        text: '',
        formatter: (cellContent, row) => {
          return this.renderRemoveButton(row);
        }
      }
    ];

    return (
      <ToolkitProvider
        keyField="id"
        data={this.props.categories}
        columns={columns}
        search
      >
        {props => (
          <div>
            <SearchBar {...props.searchProps} />
            <ClearSearchButton
              {...props.searchProps}
              className="clear-search-btn"
            />
            <hr />
            <BootstrapTable
              {...props.baseProps}
              bootstrap4
              hover={true}
              cellEdit={cellEditFactory({
                mode: 'dbclick',
                afterSaveCell: this.onAfterSaveCategory,
                autoSelectText: true
              })}
              noDataIndication="Категории не найдены"
            />
          </div>
        )}
      </ToolkitProvider>
    );
  }
}
