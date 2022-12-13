import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Card, Button } from 'react-bootstrap';
import Axios from 'axios';
import './Home.css';

export function EventsCards() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        Axios.get(`api/event/searchevents`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<Events info={Data} navigate={useNavigate()}></Events>)
}

export class Events extends Component {
    static displayName = Events.name;

    constructor(props) {
        super(props);
        console.log(this.props)
    }

    eventsDetails = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const event_id = event.target.value;
        Axios.get(`api/event/get?guid=${event_id}`, {
        }).then((response) => {
            console.log(response.data);
            localStorage.setItem("event_data", JSON.stringify(response.data))
            this.props.navigate("/EventDetails")
        });
    }

    render() {
        if (this.props.info.length > 0) {
            return this.props.info.map(value => {
                let startdate = new Date(value.startdate).toDateString();
                let enddate = new Date(value.enddate).toDateString();
                return (
                    <div>
                        <Card>
                            <Card.Img variant="top" src="../images/Events/beach.jpg" />
                            <Card.Body>
                                <Card.Title>Event: {value.name} </Card.Title>
                                <Card.Text>Organization: {value.organizationName}</Card.Text>
                                <Card.Text>{startdate} - {enddate}</Card.Text>
                                <Card.Text>{value.categoryName}</Card.Text>
                                <Button variant="primary" value={value.guid} onClick={e => this.eventsDetails(e, "value")} >Event Detail</Button>
                            </Card.Body>
                        </Card>
                    </div>
                );
            });
        }
        else {
            return (<h1>No Events</h1>)
        }
    }
}
