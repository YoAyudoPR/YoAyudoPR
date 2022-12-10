
import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Card from 'react-bootstrap/Card';
import Axios from 'axios';





export function EventDetailsNavigate(props) {
    return (<EventDetails navigate={useNavigate()}></EventDetails>)
}

export default class EventDetails extends Component {
    

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }


    render() {
        return (<>
            <div className="ED-bg-img">
                    <div className="mb-4 pt-4 ED-container">

                <Row >
                    <Col xs={ 5 }>
                        <img className="ED-img" src="../images/beach2.jpg" alt="Event Picture" />
                    </Col>
                    <Col xs={4} className="ED-info-column">
                        <Card className="p-4" style={{ width: '18rem' }}>

                        <Row>
                            <h1>Event Details</h1>
                            <Col>
                                <h5>Name</h5>
                                <p>*EVENT NAME HERE*</p>
                            </Col>
                        </Row>

                       
                        <Row>
                            <Col>
                                <h5>Timeline</h5>
                                <p className="ED-make-block">*EVENT START DATE*</p>
                                <p>*EVENT END DATE*</p>
                            </Col>
                        </Row>
                        </Card>
                    </Col>
                </Row>

                <Row className="mt-4 mb-4">
                    <Col>
                        <h5>Desciption</h5>
                        <p className="ED-make-block">*It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).*</p>
                    </Col>
                </Row>

                </div>
            </div>
        </>);
    }
}