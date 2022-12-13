import React, { Component, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Col, Row } from 'react-bootstrap';
import Axios from 'axios';


export function ProfileNavigate(props) {
    const [Data, setData] = useState([]);

    useEffect(() => {
        const user_id = localStorage.getItem('guid')
        Axios.get(`api/user/get?guid=${user_id}`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);
    return (<Profile info={Data} navigate={useNavigate()}></Profile>)
}


export default class Profile extends Component {
    static displayName = Profile.name;

    constructor(props) {
        super(props);
    }

    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    render() {
        return (<>
            <h1 className="P-h1">Account Profile</h1>
            <div className=" mb-4 P-container">
                <Row>
                    <Col xs={2}><img className="profile-img" src="../images/profile-pic.jpg" alt="Profile picture" /> </Col>
                    <Col xs={ 3 }>
                        <Button variant="outline-success">Upload</Button>{' '}
                        <p className="P-small-text">Your picture will be shown to everyone! Make it count.</p>
                    </Col>
                </Row>
            </div>

            <div className=" mb-4 P-container">
                <Row>
                    <Col>
                        <h5>Name</h5>
                        <p>{this.props.info.firstname} {this.props.info.initial} {this.props.info.lastname} {this.props.info.secondLastname}</p>
                    </Col>
                    <Col><Button variant="outline-success">Edit</Button>{' '}</Col>
                </Row>
                   
                <Row>
                    <Col>
                        <h5>Email</h5>
                        <p>{this.props.info.email}</p>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <h5>Phone</h5>
                        <p>{this.props.info.phone}</p>
                    </Col>
                    <Col><Button variant="outline-success">Edit</Button>{' '}</Col>
                </Row>

                <Row>
                    <Col>
                        <h5>Password</h5>
                        <p>******************</p>
                    </Col>
                    <Col><Button variant="outline-success">Edit</Button>{' '}</Col>
                </Row>

            </div>

            <div className="mb-4 P-container">
                <h1>Active/Participating Events</h1>

                <Row>
                    <Col>
                        <h5>Name</h5>
                        <p>*EVENT NAME HERE*</p>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <h5>Timeline</h5>
                        <p className="P-make-block">*EVENT START DATE*</p>
                        <p>*EVENT END DATE*</p>
                    </Col>
                </Row>

            </div>

        </>);
        

    }

}