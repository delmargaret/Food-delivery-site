import React, { Component } from "react";
import { Form } from "react-bootstrap";
import BootstrapTable from "react-bootstrap-table-next";
import ToolkitProvider, { Search } from "react-bootstrap-table2-toolkit";
import { STATUSES } from "./catering-facilities-statuses";
import CateringFacilitiesService from "../../services/catering-facilities-service";
import emptyIcon from "./../../empty.png";

const { SearchBar, ClearSearchButton } = Search;

export default class CateringFacilitiesList extends Component {
  constructor(props) {
    super(props);

    this.changeStatus = this.changeStatus.bind(this);
    this.handleImageChange = this.handleImageChange.bind(this);
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

  handleImageChange(e, id) {
    e.preventDefault();

    let reader = new FileReader();
    let file = e.target.files[0];

    reader.onloadend = () => {
      CateringFacilitiesService.updateIcon(id, reader.result);
    };

    reader.readAsDataURL(file);
  }

  renderIcon(url, id) {
    const iconUrl = url === "" ? emptyIcon : url;
    return (
      <div>
        <label>
          <input
            hidden={true}
            type="file"
            id="file"
            accept="image/*"
            onChange={e => this.handleImageChange(e, id)}
          />
          <img height="150px" alt="" src={iconUrl} />
        </label>
        <span
          className="close"
          onClick={() => {
            CateringFacilitiesService.updateIcon(id, "");
          }}
        >
          x
        </span>
      </div>
    );
  }

  render() {
    const columns = [
      {
        dataField: "id",
        text: "ID",
        hidden: true
      },
      {
        dataField: "cateringFacilityName",
        text: "Заведения",
        align: "left",
        headerAlign: "left",
        sort: true,
        formatter: (cellContent, row) => {
          return (
            <a href={`/catering-facilities/edit/${row.id}`}>{cellContent}</a>
          );
        }
      },
      {
        dataField: "cateringFacilityIconUrl",
        text: "Иконка",
        sort: true,
        formatter: (cellContent, row) => {
          return this.renderIcon(cellContent, row.id);
        }
      },
      {
        dataField: "cateringFacilityStatus",
        text: "Статус",
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
