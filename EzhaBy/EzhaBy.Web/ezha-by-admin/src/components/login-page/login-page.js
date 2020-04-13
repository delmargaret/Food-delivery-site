import React, { Component } from "react";
import { Form, Col, Row, Button } from "react-bootstrap";

import Emitter from "../../services/event-emitter";
import LoginService from "../../services/login-service";
import { USER_LOGGED } from "../../services/login-service";

export default class LoginPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      validated: false,
    };

    this.emailInput = React.createRef();
    this.passwordInput = React.createRef();

    this.formIsValid = false;

    this.onLogin = this.onLogin.bind(this);
  }

  onLogin(event) {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;

    this.formIsValid = false;

    if (form.checkValidity()) {
      this.formIsValid = true;
    }

    this.setState({
      validated: true,
    });
  }

  async componentDidUpdate(prevProps, prevState, snapshot) {
    if (this.formIsValid) {
      const isAuthenticated = await LoginService.setUser(
        this.emailInput.current.value,
        this.passwordInput.current.value
      );

      if (!isAuthenticated) return;

      Emitter.emit(USER_LOGGED, {});
    }
  }

  render() {
    const { validated } = this.state;

    return (
      <React.Fragment>
        <br />
        <br />
        <Row>
          <Col></Col>
          <Col sm="7">
            <Form
              noValidate
              validated={validated}
              onSubmit={this.onLogin}
              className="app-form"
            >
              <Row>
                <Form.Group as={Col}>
                  <Form.Label>E-mail</Form.Label>
                  <Form.Control ref={this.emailInput} type="email" required />
                  <Form.Control.Feedback type="invalid">
                    Введите e-mail
                  </Form.Control.Feedback>
                </Form.Group>
              </Row>
              <Row>
                <Form.Group as={Col}>
                  <Form.Label>Пароль</Form.Label>
                  <Form.Control
                    ref={this.passwordInput}
                    type="password"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Введите пароль
                  </Form.Control.Feedback>
                </Form.Group>
              </Row>
              <Button type="submit" className="btn-red">
                Вход
              </Button>
            </Form>
          </Col>
          <Col></Col>
        </Row>
      </React.Fragment>
    );
  }
}
