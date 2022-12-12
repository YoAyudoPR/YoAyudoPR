import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom';
import './NavMenu.css';

export function LogOutNavigate() {
    return (<NavMenu navigate={useNavigate()}></NavMenu>)
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

    logout = (event) => {
        event.preventDefault();
        localStorage.clear();
        this.props.navigate("/Login");
    }

    render() {
        let tab;
        let isLoggedIn;
        if (localStorage.getItem("guid") == null) {
            isLoggedIn = false;
        } else {
            isLoggedIn = true;
            if (localStorage.getItem("organizationGuid") == null) {
                tab = <NavLink tag={Link} className="text-light" to="/Home">Create Organization</NavLink>
            } else {
                tab = <NavLink tag={Link} className="text-light" to="/Home">Organization Profile</NavLink>
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

                            {isLoggedIn && tab}
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
