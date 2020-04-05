import React, { Component } from "react";
import TagsService, { TAG_LIST_UPDATED } from "../../services/tags-service";
import TagsList from "./tags-list";
import AddTagForm from "./add-tag-form";
import Emitter from "../../services/event-emitter";
import "./tags.css";
import leaves from "./../../leaves.png";
import { Row, Col } from "react-bootstrap";

export default class TagsPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tags: []
    };

    this.getTags = this.getTags.bind(this);
  }

  getTags() {
    TagsService.getTags().then(result => {
      this.setState({ tags: result.data });
    });
  }

  componentDidMount() {
    this.getTags();
    Emitter.on(TAG_LIST_UPDATED, _ => this.getTags());
  }

  componentWillUnmount() {
    Emitter.off(TAG_LIST_UPDATED);
  }

  render() {
    return (
      <div>
        <Row>
          <Col>
            {" "}
            <img alt="" src={leaves} className="leaves-left" />
          </Col>
          <Col xs={8}>
            <AddTagForm />
            <div id="tags-list">
              <TagsList tags={this.state.tags} />
            </div>
          </Col>
          <Col>
            <img alt="" src={leaves} className="leaves-right" />
          </Col>
        </Row>
      </div>
    );
  }
}
