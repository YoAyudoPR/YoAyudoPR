import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';


export function CreateOrganizationNavigate(props) {


    return (<CreateOrganization navigate={useNavigate()}></CreateOrganization>)
}

export default class CreateOrganization extends Component {


    render() {

        return (<>

            <h1>CREATE AN ORGANIZATION!</h1>
                <h6>create the page first tho...</h6>
        </>);
    }

}
