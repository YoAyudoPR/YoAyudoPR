import { Component } from "react";
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import Axios from 'axios';

export function AddHoursNavigate(props) {
    return (<AddHours navigate={useNavigate()}></AddHours>);
}

export default class AddHours extends Component {
    static displayName = AddHours.name;

    constructor(props) {
        super(props);
        this.state = { activityLogGuid: localStorage.getItem("activityLogGuid"), Hours: 0 };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event, field) {
        const result = event.target.value.replace(/\D/g, '');
        this.setState({ [field]: result });
    }

    saveHours = (event) => {
        event.preventDefault();
        console.log(this.state.activityLogGuid);
        console.log(this.state.Hours);
        Axios.get("api/activity/create", {
            guid: this.state.activityLogGuid,
            hoursVolunteered: this.state.hours
        }).then((response) => {
            console.log(response.data);
            localStorage.removeItem("activityLogGuid");
            this.props.navigate('/EventDetails');
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }

    render() {
        return (<>
            <div className="mt-4">
                <Container>
                    <Form className="form">
                        <Row className="mb-3">
                            <h1>Add hours to volunteers</h1>
                            <Form.Group xs={6} as={Col}>
                                <Form.Label>How many hours?</Form.Label>
                                <Form.Control type="text" placeholder="Hours" onChange={(e) => this.handleChange(e, "Hours")}  value={this.state.Hours} />
                            </Form.Group>
                        </Row>

                        <Button className="mt-3" variant="primary" onClick={this.saveHours}>Submit Hours</Button>
                    </Form>
                </Container>
            </div>
        </>);
    }
}