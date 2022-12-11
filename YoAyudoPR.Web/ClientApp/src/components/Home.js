import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Button, Carousel } from 'react-bootstrap';
import { Events, EventsCards } from "./Events.js";
import Axios from 'axios';
import './Home.css';

export function HomeUserDetails() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        const user_id = localStorage.getItem('guid')
        Axios.get(`api/user/get?guid=${user_id}`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<Home info={Data} navigate={useNavigate()}></Home>)
}

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
    }

    render() {
        return (
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
                {/*Testing*/}
                <div>
                    <h1>Welcome: {String(this.props.info.firstName)} </h1>
                    {/*<Button variant="primary" value={false} onClick={this.logout}>Log out</Button>*/}
                </div>
                {/*++++++++*/}
                <div>
                    <h1>Events and Activities</h1>
                    <EventsCards />
                </div>
            </body>
        );
    }
}
