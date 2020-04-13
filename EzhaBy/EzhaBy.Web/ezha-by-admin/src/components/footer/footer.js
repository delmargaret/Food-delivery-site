import React, { Component } from "react";
import { Navbar } from "react-bootstrap";

export default class FooterComponent extends Component {
  render() {
    return (
      <Navbar fixed="bottom" bg="light" className="footer">
        <Navbar.Collapse className="justify-content-end">
          <Navbar.Text>Ezha.by@{new Date().getFullYear()}</Navbar.Text>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
