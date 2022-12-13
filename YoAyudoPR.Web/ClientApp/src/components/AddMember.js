import { Component } from "react";
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { RoleDropdown } from './Role'
import { UserDropdown } from './User'
import Axios from 'axios';

export function AddMemberNavigate(props) {
    return (<AddMember navigate={useNavigate()}></AddMember>);
}

export default class AddMember extends Component {
    static displayName = AddMember.name;

    constructor(props) {
        super(props);
    }

    addMember = (event) => {
        event.preventDefault();
        const orgGuid = localStorage.getItem("selectedOrgGuid");
        const memberGuid = localStorage.getItem("memberGuid");
        const roleId = localStorage.getItem("roleId");
        Axios.post("api/member/create", {
            userGuid: memberGuid,
            organizationGuid: orgGuid,
            roleId: roleId
        }).then((response) => {
            console.log(response.data);
            this.props.navigate('/OrganizationProfile');
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
                        
                        <h1>Add member to your organization</h1>
                        <UserDropdown />
                        <RoleDropdown />
                        
                        <Button className="mt-3" variant="primary" onClick={this.addMember}>Add Member</Button>
                    </Form>
                </Container>
            </div>
        </>);
    }
}