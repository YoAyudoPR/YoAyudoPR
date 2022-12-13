import React, { Component, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
import { Dropdown, Form } from 'react-bootstrap';
import Axios from 'axios';
import './Home.css';


export function CategoryDropdown() {
    const [Data, setData] = useState([]);

    useEffect(() => {
        Axios.get(`api/event/getcategories`, {
        }).then((response) => {
            console.log(response);
            setData(response.data)
        });
    }, []);

    return (<Category info={Data} navigate={useNavigate()}></Category>)
}

export class Category extends Component {
    static displayName = Category.name;

    constructor(props) {
        super(props);
        this.state = { id: 0, name: '' };
    }

    selectCategory = (event) => {
        localStorage.setItem("category", event)
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
            <a href="" ref={ref} onClick={(e) => {e.preventDefault(); onClick(e); }}>{children} &#x25bc;</a>
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
            <Dropdown>
                <Dropdown.Toggle as={CustomToggle} id="dropdown-custom-components">SELECT CATEGORY</Dropdown.Toggle>
                <Dropdown.Menu as={CustomMenu}>
                    {this.props.info.map((value) => (
                        <Dropdown.Item key={value.categoryId} onClick={() => this.selectCategory(value.categoryId)}>{value.categoryName}</Dropdown.Item>
                    ))}
                </Dropdown.Menu>
            </Dropdown>
        )

    }
}
