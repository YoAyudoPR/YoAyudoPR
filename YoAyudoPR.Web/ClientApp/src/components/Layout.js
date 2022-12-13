import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu, LogOutNavigate } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
        <>
          <LogOutNavigate />
          {this.props.children}
    </>);
  }
}
