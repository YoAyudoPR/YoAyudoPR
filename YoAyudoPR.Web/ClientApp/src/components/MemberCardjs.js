import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import { CreateEventNavigate } from "./CreateEvent.js";
import { Category, CategoryDropdown } from './Category'
import Axios from 'axios';
import './CreateEvent.css';


export function MemberCard(props) {
    const [MemberData, setMemberData] = useState([]);

    useEffect(() => {
        const org_id = localStorage.getItem("orgGuid")
        Axios.get(`api/member/getorganizationmembers?organizationGuid=${org_id}`, {
        }).then((response) => {
            console.log(response);
            setMemberData(response.data)
        });
    }, []);

    return (<Member member={MemberData} navigate={useNavigate()}></Member>)
}

export default class Member extends Component {
    static displayName = Member.name;

    constructor(props) {
        super(props);
        this.state = { Data: [] };
        console.log(this.props)
    }

    ToProfile = (event) => {
        event.preventDefault();
        console.log(event.target.value)
        const userGuid = event.target.value;
        localStorage.setItem("userGuid", userGuid);
        this.props.navigate("/Profile")
    }

    render() {
        if (this.props.member.length > 0) {
            return this.props.member.map(value => {
                return (
                 
                    <Col class="col-4 justify-content-center">
                            
                                <Card style={{ width: '18rem' }}>
                                    <Card.Img variant="top" src="../images/profile-pic.jpg" />
                                    <Card.Body>
                                        <Card.Title>{value.userName}</Card.Title>
                                        <Card.Text>{value.roleName}</Card.Text>
                                        <Button variant="primary" value={value.userGuid} onClick={this.ToProfile}>PROFILE</Button>
                                    </Card.Body>
                                </Card>
                            </Col>
                      
                );
            });
        }
        else {
            return (<h1>No Member</h1>)
        }
    }

}

