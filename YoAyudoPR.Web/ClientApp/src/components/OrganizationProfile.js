import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';


export function OrganizationProfileNavigate(props) {


    return (<OrganizationProfile navigate={useNavigate()}></OrganizationProfile>)
}


export default class OrganizationProfile extends Component {


    render() {

        return (<>

            <div className="OP-first-div">
                <h1>ORGANIZATION NAME</h1>
                <Row>
                    <Col>
                        <h5>Description</h5>
                        <p>*It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).*</p>
                    </Col>
                </Row>
            </div>

            <div className="mb-4 OP-second-div">
                {/*Add loop for multiple events!*/}
                <h2>Events</h2>
                <Row className="d-flex justify-content-center mb-4">
                    <Col xs={4} className="mr-4">
                        <Card style={{ width: '18rem' }}>
                        <Card.Img variant="top" src="../images/teamwork.jpg" />
                            <Card.Body>
                                <Card.Title>*EVENT NAME*</Card.Title>
                                <Button variant="primary">LINK EVENT DETAILS</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col xs={4}>
                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src="../images/teamwork.jpg" />
                            <Card.Body>
                                <Card.Title>*EVENT NAME*</Card.Title>
                                <Button variant="primary">LINK EVENT DETAILS</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col xs={4}>
                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src="../images/teamwork.jpg" />
                            <Card.Body>
                                <Card.Title>*EVENT NAME*</Card.Title>
                                <Button variant="primary">LINK EVENT DETAILS</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
                {/*////////////////////////////////*/}


                {/*Add loop for multiple members!*/}
                <h2>Members</h2>
                <Row className="d-flex justify-content-center">
                    <Col xs={ 4 }>
                      
                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src="../images/profile-pic.jpg" />
                            <Card.Body>
                                <Card.Title>*MEMBERS NAME*</Card.Title>
                                <Button variant="primary">LINK PROFILE</Button>
                            </Card.Body>
                            </Card>
                    </Col>
                    <Col xs={4}>

                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src="../images/profile-pic.jpg" />
                            <Card.Body>
                                <Card.Title>*MEMBERS NAME*</Card.Title>
                                <Button variant="primary">LINK PROFILE</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col xs={4}>

                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src="../images/profile-pic.jpg" />
                            <Card.Body>
                                <Card.Title>*MEMBERS NAME*</Card.Title>
                                <Button variant="primary">LINK PROFILE</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
                {/*////////////////////////////////*/}
            </div>
           

        </>);

    }

}

