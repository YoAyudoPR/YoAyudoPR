import React, { Component } from 'react';
import { Card, Button, Carousel } from 'react-bootstrap';
import Login, { LogNavigate, LogInStatus } from "../components/Login";
import { EventsCards } from "../components/EventsCards.js";
import './Home.css';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <body>
                {/*Testing*/}
                <div>
                    <h1>Status: {String(LogInStatus)} </h1>
                    <Button variant="primary" value={false} onClick={LogInStatus}>Log out</Button>
                </div>
                {/*++++++++*/}
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
                    <EventsCards/>
                </div>
            </body>
        );
    }
}
