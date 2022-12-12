import React, { Component,useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import Collapse from 'react-bootstrap/Collapse';
import { CreateEventNavigate } from "./CreateEvent.js";
import Axios from 'axios';
import './CreateEvent.css';


export function CreateOrganizationNavigate(props) {

    return (<CreateOrganization navigate={useNavigate()}></CreateOrganization>)
}

function ShowCE(){
    const [open, setOpen] = useState(false);
    return (<>
        <h6  className="mt-4 CO-cursor"
            onClick={() => setOpen(!open)}
            aria-controls="example-collapse-text"
            aria-expanded={open}
        >
            Create an event?
        </h6>
        <div style={{ minHeight: '100px' }}>
            <Collapse in={open} dimension="width">
                <div id="example-collapse-text">
                    <Card body style={{ width: 'auto' }}>
                        <CreateEventNavigate /> 
                    </Card>
                </div>
            </Collapse>
        </div>

    </>);
}

export default class CreateOrganization extends Component {
    
   

    render() {
        
        return (<>


            <div className="CO-div mt-4">
                <h1>CREATE AN ORGANIZATION!</h1>
                <Form>
                    <Form.Group xs={ 6 } as={Col}>
                        <Form.Label>Organization Name</Form.Label>
                        <Form.Control type="text" placeholder="Organization name" />
                    </Form.Group>

                    <Form.Group xs={ 6 } as={Col}>
                        <Form.Label>Description</Form.Label>
                        <Form.Control type="text" placeholder="Describe your event!" />
                    </Form.Group>
                    <Button className="mt-2" variant="primary" size="sm">
                        Submit</Button>
                </Form>
            </div>
            
        </>);
    }

}
