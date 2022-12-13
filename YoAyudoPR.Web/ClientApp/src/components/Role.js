import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Dropdown, Form } from 'react-bootstrap';
import Axios from 'axios';


export function RoleDropdown() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        Axios.get(`/api/member/getroles`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<Role info={Data} navigate={useNavigate()}></Role>)
}

export class Role extends Component {
    static displayName = Role.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form.Group>
                <Form.Label>Select Role</Form.Label>
                <Form.Control
                    as="select"
                    onChange={e => {
                        console.log(e.target.value);
                        localStorage.setItem("roleId", e.target.value);
                    }}
                >
                    <option >Select Role</option>
                    {this.props.info.map((value) => (
                        <option key={value.roleId} value={value.roleId}>{value.roleName}</option>
                    ))}
                </Form.Control>
            </Form.Group>
        )

    }
}
