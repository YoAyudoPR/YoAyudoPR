import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Row, Card, } from 'react-bootstrap/';
import Axios from 'axios';
import './CreateEvent.css';
import { EventUserCard } from './EventUser';

export function OrgEventDetailsNavigate(props) {
    const event_info = JSON.parse(localStorage.getItem("event_data"))
    return (<OrgEventDetails info={event_info} navigate={useNavigate()} ></OrgEventDetails>)
}

export default class OrgEventDetails extends Component {
    static displayName = OrgEventDetails.name;

    constructor(props) {
        super(props);
    }

    goHome = (event) => {
        this.props.navigate("/Home");
    }

    render() {
        let startdate = new Date(this.props.info.startdate).toUTCString();
        let enddate = new Date(this.props.info.enddate).toUTCString();
        return (<>
            <div className="ED-bg-img mb-4">
                <div className="mb-4 pt-4 ED-container">
                    <Row >
                        <Col xs={5}>
                            <img className="ED-img" src="../images/beach2.jpg" alt="Event Picture" />
                        </Col>
                        <Col xs={4} className="ED-info-column">
                            <Card className="p-4" style={{ width: '18rem' }}>
                                <Row>
                                    <h1>Event Details</h1>
                                    <Col>
                                        <h5>Name</h5>
                                        <p>{this.props.info.name}</p>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                        <h5>Timeline</h5>
                                        <p className="ED-make-block">{startdate}</p>
                                        <p>{enddate}</p>
                                    </Col>
                                </Row>

                                <Row>
                                    <Col>
                                        <h5>Capacity</h5>
                                        <p className="ED-make-block">{this.props.info.capacity}</p>
                                    </Col>
                                </Row>

                                <Row>
                                    <Col>
                                        <h5>Address</h5>
                                        <p className="ED-make-block">{this.props.info.address}</p>
                                    </Col>
                                </Row>

                                <Row>
                                    <Col>
                                        <h5>Website</h5>
                                        <p className="ED-make-block">{this.props.info.websiteurl}</p>
                                    </Col>
                                </Row>


                            </Card>
                        </Col>
                    </Row>
                    <Row className="mt-4 mb-4">
                        <Col>
                            <h5 className="ED-white-text">Desciption</h5>
                            <p className="ED-white-text ED-make-block">{this.props.info.description}</p>
                        </Col>
                    </Row>
                    <EventUserCard/>
                    <Row>
                        <Button className="mb-4" variant="primary" onClick={this.goHome} >Go Home</Button>
                    </Row>
                </div>
            </div>

        </>);
    }
}