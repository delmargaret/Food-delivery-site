import React, { Component } from "react";
import BootstrapTable from "react-bootstrap-table-next";
import ToolkitProvider, { Search } from "react-bootstrap-table2-toolkit";
import { Row, Col, Button, Form, Modal } from "react-bootstrap";

import { REQUEST_STATUSES } from "./request-statuses";
import RequestsService, {
  COURIER_LIST_UPDATED
} from "../../services/requests-service";
import Emitter from "../../services/event-emitter";
import { VEHICLE_TYPES } from "./vehicle-types";

const { SearchBar, ClearSearchButton } = Search;

export default class CourierRequestsPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      requests: [],
      show: false,
      currentCourier: {},
      currentStatus: null
    };

    this.getCourierRequests = this.getCourierRequests.bind(this);
    this.changeStatus = this.changeStatus.bind(this);
    this.sendEmail = this.sendEmail.bind(this);
    this.setShow = this.setShow.bind(this);
    this.subjectInput = React.createRef();
    this.bodyInput = React.createRef();
  }

  componentDidMount() {
    this.getCourierRequests();
    Emitter.on(COURIER_LIST_UPDATED, _ => this.getCourierRequests());
  }

  getCourierRequests() {
    RequestsService.getCourierRequests().then(result => {
      this.setState({ requests: result.data });
    });
  }

  setShow(show) {
    this.setState({ show: show });
  }

  changeStatus(courier, status) {
    this.setState({ currentCourier: courier, currentStatus: status });
    this.setShow(true);
  }

  sendEmail(id, status, email) {
    RequestsService.sendEmail(
      email,
      this.subjectInput.current.value,
      this.bodyInput.current.value
    );
    RequestsService.changeCourierStatus(id, status);
    this.setShow(false);
  }

  renderRequestButton(courier) {
    switch (courier.requestStatus) {
      case REQUEST_STATUSES.New:
        return (
          <div>
            <Row>
              <Col sm="2">
                <Button
                  onClick={() =>
                    this.changeStatus(courier, REQUEST_STATUSES.Accepted)
                  }
                >
                  Принять
                </Button>
              </Col>
              <Col sm="2">
                <Button
                  onClick={() =>
                    this.changeStatus(courier, REQUEST_STATUSES.Rejected)
                  }
                >
                  Отклонить
                </Button>
              </Col>
            </Row>
          </div>
        );
      case REQUEST_STATUSES.Accepted:
        return <div>Принят</div>;
      case REQUEST_STATUSES.Rejected:
        return <div>Отклонен</div>;
      default:
        return "";
    }
  }

  render() {
    const requestStatus =
      this.state.currentStatus === REQUEST_STATUSES.Accepted
        ? "Принять"
        : "Отклонить";
    const courier = this.state.currentCourier;

    const columns = [
      {
        dataField: "id",
        text: "ID",
        hidden: true
      },
      {
        dataField: "courierName",
        isDummyField: true,
        text: "Имя курьера",
        sort: true,
        formatter: (cellContent, row) => {
          return `${row.surname} ${row.name} ${row.patronymic}`;
        }
      },
      {
        dataField: "vehicleType",
        text: "Тип транспорта",
        align: "left",
        headerAlign: "left",
        sort: true,
        formatter: (cellContent, row) => {
          return VEHICLE_TYPES.find(type => type.id === cellContent).name;
        }
      },
      {
        dataField: "fuelConsumption",
        text: "Расход топлива",
        align: "left",
        headerAlign: "left",
        sort: true
      },
      {
        dataField: "phone",
        text: "Телефон",
        align: "left",
        headerAlign: "left",
        sort: true
      },
      {
        dataField: "email",
        text: "Почта",
        align: "left",
        headerAlign: "left",
        sort: true
      },
      {
        dataField: "requestStatus",
        text: "Статус",
        sort: true,
        formatter: (cellContent, row) => {
          return this.renderRequestButton(row);
        }
      }
    ];

    return (
      <div>
        <ToolkitProvider
          keyField="id"
          data={this.state.requests}
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

        <Modal show={this.state.show} onHide={() => this.setShow(false)}>
          <Modal.Header closeButton>
            <Modal.Title>{requestStatus}</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            <div>
              <p>Email: {courier.email}</p>
              <Row>
                <Col sm="2">Subject:</Col>
                <Col>
                  <Form.Group>
                    <Form.Control ref={this.subjectInput} type="text" />
                  </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                  <Form.Group>
                    <Form.Control
                      ref={this.bodyInput}
                      as="textarea"
                      defaultValue={`Здравствуйте, ${courier.name} ${courier.patronymic}! \n`}
                    />
                  </Form.Group>
                </Col>
              </Row>
            </div>
          </Modal.Body>

          <Modal.Footer>
            <Button
              variant="primary"
              onClick={() =>
                this.sendEmail(
                  courier.id,
                  this.state.currentStatus,
                  courier.email
                )
              }
            >
              Отправить
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}
