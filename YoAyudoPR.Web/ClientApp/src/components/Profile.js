import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Axios from 'axios';


export function ProfileNavigate(props) {
    return (<Profile navigate={useNavigate()}></Profile>)
}



export default class Profile extends Component {


    handleChange(event, field) {
        this.setState({ [field]: event.target.value });
    }

    render() {

    
        const Card = () => {
            return (
                <div>
                    <link
                        rel="stylesheet"
                        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
                    />
                    <div className="card">
                        <img src="img.jpg" alt="Juan" style={{ width: "100%" }} />
                        <h1>Juan Perez</h1>
                        <p className="title">CEO &amp; Founder, Example</p>
                        <p>Harvard University</p>
                        <a href="#">
                            <i className="fa fa-dribbble" />
                        </a>
                        <a href="#">
                            <i className="fa fa-twitter" />
                        </a>
                        <a href="#">
                            <i className="fa fa-linkedin" />
                        </a>
                        <a href="#">
                            <i className="fa fa-facebook" />
                        </a>
                        <p>
                            <button>Contact</button>
                        </p>
                    </div>
                </div>
            );
        };
        

    }

}