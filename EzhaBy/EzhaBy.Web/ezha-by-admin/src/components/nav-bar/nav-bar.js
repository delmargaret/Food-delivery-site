import React, { Component } from 'react';
import { Navbar } from 'react-bootstrap';
import logo from './../../logo9.png';

export default class NavbarComponent extends Component {
  render() {
    return (
      <Navbar bg="light">
        <Navbar.Brand id="logo-img">
          <img src={logo} width="120" alt="React Bootstrap logo" />
        </Navbar.Brand>
      </Navbar>
    );
  }
}
