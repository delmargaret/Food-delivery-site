import React, { Component } from 'react';
import TagsService from '../../services/tags-service';
import { Button, Form } from 'react-bootstrap';

export default class TagsItem extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isAssigned: false,
      isEditClicked: false
    };
  }

  componentDidMount() {
    TagsService.checkTagIsAssigned(this.props.tagId).then(result => {
      this.setState({ isAssigned: result.data });
    });
  }

  onDeleteTag(id) {
    TagsService.deleteTag(id).then();
  }

  renderEditInput(tagName) {
    return (
      <Form>
        <Form.Group>
          <Form.Control type="text" placeholder="tag name" defaultValue={tagName}/>
        </Form.Group>
      </Form>
    );
  }

  renderEditButton() {}

  renderTagItem() {
    let tagNameField = this.state.isEditClicked
      ? this.renderEditInput(this.props.tagName)
      : this.props.tagName;
    let editButton = this.state.isEditClicked ? (
      <Button
        onClick={() => this.setState({ isEditClicked: false })}
        variant="outline-secondary"
      >
        Save
      </Button>
    ) : (
      <Button
        onClick={() => this.setState({ isEditClicked: true })}
        variant="outline-secondary"
      >
        Edit
      </Button>
    );
    let removeButton = this.state.isAssigned ? (
      ''
    ) : (
      <Button
        onClick={() => this.onDeleteTag(this.props.tagId)}
        variant="outline-danger"
      >
        Remove
      </Button>
    );
    return (
      <tr>
        <td>{tagNameField}</td>
        <td>{editButton}</td>
        <td>{removeButton}</td>
      </tr>
    );
  }

  render() {
    return this.renderTagItem();
  }
}
