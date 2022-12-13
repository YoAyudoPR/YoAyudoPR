import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Dropdown, Form, Select } from 'react-bootstrap';
import Axios from 'axios';


export function UserDropdown() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        Axios.get(`/api/user/searchusers`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<User info={Data} navigate={useNavigate()}></User>)
}

export class User extends Component {
    static displayName = User.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (<>
            <Form.Group>
                <Form.Label>Select User</Form.Label>
                <Form.Control
                    as='select'
                    onChange={e => {
                        console.log(e.target.value);
                        localStorage.setItem("memberGuid", e.target.value);
                    }}
                >
                    <option>Select User</option>
                    {this.props.info.map((value) => (
                        <option key={value.guid} value={value.guid}>{value.fullName}</option>
                    ))}
                </Form.Control>
            </Form.Group>
        </>
            
        )

    }
}
