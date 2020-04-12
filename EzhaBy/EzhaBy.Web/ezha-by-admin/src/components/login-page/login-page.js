import React, { Component } from "react";
import { Form, Col, Row, Button } from "react-bootstrap";
import LoginService from "../../services/login-service";
import { Redirect } from "react-router-dom";

export default class LoginPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      validated: false,
      needRedirect: false,
    };

    this.emailInput = React.createRef();
    this.passwordInput = React.createRef();
    this.onLogin = this.onLogin.bind(this);
  }

  async onLogin(event) {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;
    let needRedirect = false;

    if (form.checkValidity()) {
      needRedirect = true;

      await LoginService.setUser(
        this.emailInput.current.value,
        this.passwordInput.current.value
      );
      this.props.logIn();
    }

    this.setState({
      validated: true,
      needRedirect: needRedirect,
    });
  }

  render() {
    const { validated, needRedirect } = this.state;

    const redirectElement = <Redirect to='/' />;

    const formElement = (
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

    return needRedirect ? redirectElement : formElement;
  }
}
