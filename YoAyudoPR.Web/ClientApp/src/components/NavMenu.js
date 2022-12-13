import React, { Component, useEffect, useState } from 'react';
import Axios from 'axios';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom';
import { Dropdown } from 'react-bootstrap';
import './NavMenu.css';

export function LogOutNavigate() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        const user_id = localStorage.getItem('guid');
        Axios.get(`/api/member/getusermemberships?userGuid=${user_id}`, {
        }).then((response) => {
            console.log(response);
            setData(response.data);
        });
    }, []);

    return (<NavMenu info={Data} navigate={useNavigate()}></NavMenu>)
}

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

    profile = (guid) => {
        localStorage.setItem("orgGuid", guid);
        this.props.navigate("/OrganizationProfile");
        window.location.reload(false);
    }

    logout = (event) => {
        event.preventDefault();
        localStorage.clear();
        this.props.navigate("/Login");
    }

    render() {
        let isLoggedIn;
        if (localStorage.getItem("guid") == null) {
            isLoggedIn = false;
        } else {
            isLoggedIn = true;
            if (localStorage.getItem("organizationGuid") == null) {
                <NavLink tag={Link} className="text-light" to="/CreateOrganization">Create Organization</NavLink>
            } else {
                <NavLink tag={Link} className="text-light" to="/OrganizationProfile">Organization Profile</NavLink>
            }
        }

        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 bg-primary" container light>
                    <NavbarBrand className="text-light" tag={Link} to="/">YoAyudoPR</NavbarBrand>
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem >
                                <NavLink tag={Link} className="text-light" to="/">Landing Page</NavLink>
                            </NavItem>
                            <NavItem >
                                <NavLink tag={Link} className="text-light" to="/Swagger">API DOCS</NavLink>
                            </NavItem>
                            {!isLoggedIn &&
                                <NavItem>
                                    <NavLink tag={Link} className="text-light" to="/Login">Login</NavLink>
                                </NavItem>
                            }

                            {isLoggedIn && 
                                <NavItem>
                                    <NavLink tag={Link} className="text-light" to="/Home">Home</NavLink>
                                </NavItem>
                            }

                            {isLoggedIn && 
                                <NavItem>
                                    <NavLink tag={Link} className="text-light" to="/Profile">Profile</NavLink>
                                </NavItem>
                            }

                            {isLoggedIn && 
                                <Dropdown>
                                    <Dropdown.Toggle id="dropdown-custom-components">My Organizations</Dropdown.Toggle>
                                    <Dropdown.Menu>
                                        {this.props.info.map((value) => (
                                            <Dropdown.Item key={value.organizationGuid} value={value.organizationGuid} onClick={() => this.profile(value.organizationGuid)}> {value.organizationName} </Dropdown.Item>
                                        ))}
                                    </Dropdown.Menu>
                                </Dropdown>
                            }
                            {isLoggedIn && 
                                <NavItem>
                                    <NavLink tag={Link} className="text-light" to="/" onClick={this.logout}>Logout</NavLink>
                                </NavItem>
                            }
                        </ul>
                    </Collapse>
                </Navbar>
            </header>
        );
    
  }
}
