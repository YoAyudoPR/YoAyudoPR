﻿import React, { Component, useState } from 'react';
import { Dropdown, Form } from 'react-bootstrap';
import Axios from 'axios';
import './Home.css';

export class Category extends Component {
    static displayName = Category.name;

    constructor(props) {
        super(props);
        this.state = { id: 0, name: '' };
    }

    getCategory = (event) => {
        event.preventDefault();
        Axios.get("api/event/getcategories", {
        }).then((response) => {
            console.log(response.data);
        }).catch((error) => {
            if (error.response) {
                console.log(error.response.data);
                alert(`Error! ${error.message}`);
            }
        });
    }

    render() {
        const CustomToggle = React.forwardRef(({ children, onClick }, ref) => (
            <a
                href=""
                ref={ref}
                onClick={(e) => {
                    e.preventDefault();
                    onClick(e);
                }}
            >
                {children}
                &#x25bc;
            </a>
        ));

        const CustomMenu = React.forwardRef(
            ({ children, style, className, 'aria-labelledby': labeledBy }, ref) => {
                const [value, setValue] = useState('');

                return (
                    <div
                        ref={ref}
                        style={style}
                        className={className}
                        aria-labelledby={labeledBy}>
                        <Form.Control
                            autoFocus
                            className="mx-3 my-2 w-auto"
                            placeholder="Type to filter..."
                            onChange={(e) => setValue(e.target.value)}
                            value={value} />
                        <ul className="list-unstyled">
                            {React.Children.toArray(children).filter(
                                (child) =>
                                    !value || child.props.children.toLowerCase().startsWith(value),
                            )}
                        </ul>
                    </div>
                );
            },
        );

        return (
            <div>
                <Dropdown>
                    <Dropdown.Toggle as={CustomToggle} id="dropdown-custom-components">SELECT CATEGORY</Dropdown.Toggle>
                    <Dropdown.Menu as={CustomMenu}>
                        {/*For Loop Needed*/}
                        <Dropdown.Item eventKey="1">Red</Dropdown.Item>
                        <Dropdown.Item eventKey="2">Blue</Dropdown.Item>
                        <Dropdown.Item eventKey="3">Orange</Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>
            </div>
        );
    }
}
