
import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
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
            <div className="img">
                <h1 class="center">Event Details</h1>
               
            </div>
        </>);
    }
}