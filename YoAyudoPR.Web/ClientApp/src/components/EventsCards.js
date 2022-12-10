import React, { Component } from 'react';
import { Card, Button } from 'react-bootstrap';
import Axios from 'axios';
import './Home.css';

export class EventsCards extends Component {
    static displayName = EventsCards.name;

    constructor(props) {
        super(props);
        this.state = { eventName: '', description: '', startDate: 0, endDate: 0, capacity: 0};
    }

    getEvents = (event) => {
        event.preventDefault();
        Axios.get("api/event/get", {
        }).then((response) => {
            console.log(response.data);
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }

    /*For Loop Needed*/
    render() {
        return (
            <div>
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
        );
    }
}
