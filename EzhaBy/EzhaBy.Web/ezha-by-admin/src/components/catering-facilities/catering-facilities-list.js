import React, { Component } from 'react';
import { Form } from 'react-bootstrap';
import BootstrapTable from 'react-bootstrap-table-next';
import ToolkitProvider, { Search } from 'react-bootstrap-table2-toolkit';
import { STATUSES } from './catering-facilities-statuses';
import CateringFacilitiesService from '../../services/catering-facilities-service';

const { SearchBar, ClearSearchButton } = Search;

export default class CateringFacilitiesList extends Component {
  constructor(props) {
    super(props);

    this.changeStatus = this.changeStatus.bind(this);
  }

  changeStatus(e, id) {
    const status = e.target.checked ? STATUSES.Active : STATUSES.Disabled;
    CateringFacilitiesService.changeStatus(id, status);
  }

  renderDisableButton(cateringFacility) {
    return (
      <div>
        <Form.Check
          type="switch"
          label="Активно"
          id={cateringFacility.id}
          onChange={e => this.changeStatus(e, cateringFacility.id)}
          defaultChecked={
            cateringFacility.cateringFacilityStatus === STATUSES.Active
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
        dataField: 'cateringFacilityName',
        text: 'Заведения',
        align: 'left',
        headerAlign: 'left',
        sort: true,
        formatter: (cellContent, row) => {
          return <a href={`/update/${row.id}`}>{cellContent}</a>;
        }
      },
      {
        dataField: 'cateringFacilityStatus',
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
        data={this.props.cateringFacilities}
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
              noDataIndication="Заведения не найдены"
            />
          </div>
        )}
      </ToolkitProvider>
    );
  }
}
