import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Button, Carousel,Row } from 'react-bootstrap';
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
        return (<>

            <div>
                    <Carousel>
                        <Carousel.Item>
                            <img className="d-block w-100" height="400" src="../images/teamwork.jpg" alt="First slide" />
                            <Carousel.Caption>
                                <h3>Working Together</h3>
                                <p>Find the event that suits you best!</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img className="d-block w-100" height="400" src="../images/forest.jpg" alt="Second slide" />
                            <Carousel.Caption>
                                <h3>Events anywhere in Puerto Rico</h3>
                                <p>Finding an experice from all over the island.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img className="d-block w-100" height="400" src="../images/beach2.jpg" alt="Third slide" />
                            <Carousel.Caption>
                                <h3>Clean, cooperate and make an impact</h3>
                                <p>Trying to get into cleaning Puerto Rico's coast? Take a look!</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
            </div>


            <div className="Home-bg-img"> 
                {/*Testing*/}
            <div className="pt-4">
                <h1 class="Home-name Home-white-text">Welcome: {String(this.props.info.firstName)}! </h1>
                </div>
            {/*++++++++*/}
                <h1 className="Home-event-h1 mb-4 Home-white-text">Events and Activities</h1>
            <Row className="ml-4">
                   

                    <EventsCards /> 
               
            </Row>
        </div>
        </>);
    }
}
