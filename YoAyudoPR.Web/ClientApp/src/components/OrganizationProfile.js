import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';
import { MemberCard } from './MemberCardjs.js';
import { OrgEventCard } from './OrganizationEvents'


export function OrganizationProfileNavigate(props) {
    const [Data, setData] = useState([]);
    const [EventData, setEventData] = useState([]);
    const [MemberData, setMemberData] = useState([]);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/organization/get?guid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<OrganizationProfile info={Data} navigate={useNavigate()}></OrganizationProfile>)
}


export default class OrganizationProfile extends Component {
    static displayName = OrganizationProfile.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

    render() {
        return (<>
            <div className="OP-bg-img pt-4">
                <div className="OP-first-div">
                    <h1 className="OP-text-white">{this.props.info.name}</h1>
                    <Row>
                        <Col className="OP-text-white">
                        <h5>Description</h5>
                        <p>{this.props.info.description}</p>
                    </Col>
                </Row>
            </div>
            <h2 className="OP-make-inline OP-text-white">Members</h2>
            <Button className="OP-button" size="sm"><a className="OP-a" href="/AddMember">Add Member</a></Button>
            <MemberCard />
                <h2 className="OP-make-inline OP-text-white">Events</h2>
                  <Button className="OP-button" size="sm"><a className="OP-a" href="/CreateEvent">Create Event</a></Button>
                <OrgEventCard />
            </div>
        </>);
    }

}

