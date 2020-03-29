import React, { Component } from 'react';
import { Form } from 'react-bootstrap';
import BootstrapTable from 'react-bootstrap-table-next';
import ToolkitProvider, { Search } from 'react-bootstrap-table2-toolkit';
import { DISH_STATUSES } from './dishes-statuses';
import DishesService from '../../services/dishes-service';

const { SearchBar, ClearSearchButton } = Search;

export default class DishesList extends Component {
  constructor(props) {
    super(props);

    this.changeStatus = this.changeStatus.bind(this);
  }

  changeStatus(e, id) {
    const status = e.target.checked ? DISH_STATUSES.InStock : DISH_STATUSES.NotAvailable;
    DishesService.changeStatus(id, status);
  }

  renderDisableButton(dish) {
    return (
      <div>
        <Form.Check
          type="switch"
          label="В наличии"
          id={dish.id}
          onChange={e => this.changeStatus(e, dish.id)}
          defaultChecked={
            dish.dishStatus === DISH_STATUSES.InStock
          }
        />
      </div>
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
        dataField: 'dishName',
        text: 'Блюда',
        align: 'left',
        headerAlign: 'left',
        sort: true,
        formatter: (cellContent, row) => {
          return <a href={`/updateDish/${row.id}/${this.props.cateringFacilityId}`}>{cellContent}</a>;
        }
      },
      {
        dataField: 'cateringFacilityCategory.categoryName',
        text: 'Категории',
        align: 'left',
        headerAlign: 'left',
        sort: true,
      },
      {
        dataField: 'dishStatus',
        text: 'Статус',
        sort: true,
        formatter: (cellContent, row) => {
          return this.renderDisableButton(row);
        }
      }
    ];

    return (
      <ToolkitProvider
        keyField="id"
        data={this.props.dishes}
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
              noDataIndication="Блюда не найдены"
            />
          </div>
        )}
      </ToolkitProvider>
    );
  }
}
