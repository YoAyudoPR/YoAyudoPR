import React, { Component, useState } from 'react';
import { Card, Button, Carousel } from 'react-bootstrap';
import './Home.css';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (<>
            <body>
                <div>
                    <Carousel>
                        <Carousel.Item>
                            <img className="d-block w-100" height="800" src="../images/teamwork.jpg" alt="First slide" />
                            <Carousel.Caption>
                                <h3>First slide label</h3>
                                <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img className="d-block w-100" height="800" src="../images/forest.jpg" alt="Second slide" />
                            <Carousel.Caption>
                                <h3>Second slide label</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img className="d-block w-100" height="800" src="../images/beach2.jpg" alt="Third slide" />
                            <Carousel.Caption>
                                <h3>Third slide label</h3>
                                <p>Praesent commodo cursus magna, vel scelerisque nisl consectetur.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
                </div>
                <div>
                    <h1>Events and Activities</h1>
                    <Card style={{ width: '18rem' }}>
                        <Card.Img variant="top" src="../images/Events/beach.jpg" />
                        <Card.Body>
                            <Card.Title>Card Title</Card.Title>
                            <Card.Text>
                                Some quick example text to build on the card title and make up the
                                bulk of the card's content.
                            </Card.Text>
                            <Button variant="primary">Go somewhere</Button>
                        </Card.Body>
                    </Card>
                </div>
            </body>
        </>);
    }
}
