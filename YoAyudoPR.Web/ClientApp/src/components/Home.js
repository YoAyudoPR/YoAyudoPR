import React, { Component } from 'react';
import { Accordion } from 'react-bootstrap';
import './Home.css';

export class Home extends Component {
  static displayName = Home.name;

  render() {
      return (
          <><h1 className = "title" >Help Puerto Rico Community</h1>
              <Accordion defaultActiveKey="0">
              <Accordion.Item eventKey="0">
                  <Accordion.Header>What is YoAyudoPR?</Accordion.Header>
                  <Accordion.Body>
                   The purpose of this project is to provide a website where high school students and college students may locate volunteer opportunities to broaden their community service experience. Allow local organizations to list their events and volunteer recruitment efforts as well.
                  </Accordion.Body>
              </Accordion.Item>
              <Accordion.Item eventKey="1">
                  <Accordion.Header>Project Synopsis</Accordion.Header>
                  <Accordion.Body>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                      eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                      minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                      aliquip ex ea commodo consequat.
                  </Accordion.Body>
                  </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>Domain Model</Accordion.Header>
                    <Accordion.Body>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                        eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                        minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                        aliquip ex ea commodo consequat.
                    </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="3">
                      <Accordion.Header>Technical Process</Accordion.Header>
                      <Accordion.Body>
                          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                          aliquip ex ea commodo consequat.
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="4">
                      <Accordion.Header>Project Plan</Accordion.Header>
                      <Accordion.Body>
                          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                          aliquip ex ea commodo consequat.
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="5">
                      <Accordion.Header>Github Repository</Accordion.Header>
                      <Accordion.Body>
                          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                          aliquip ex ea commodo consequat.
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="6">
                      <Accordion.Header>Product Demo & Website Coming Soon</Accordion.Header>
                      <Accordion.Body>
                          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
                          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                          aliquip ex ea commodo consequat.
                      </Accordion.Body>
                  </Accordion.Item>
              </Accordion></>
    );
  }
}
