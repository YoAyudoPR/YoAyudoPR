import React from "react";
import { Component } from "react";
import ReactDOM from "react-dom";
import { useNavigate } from 'react-router-dom';
import SwaggerUI from "swagger-ui-react";

import "swagger-ui-react/swagger-ui.css";

export function SwaggerNavigate(props) {
    return (<Swagger navigate={useNavigate()}></Swagger>)
}

export default class Swagger extends Component {
    render() {
        return (
            <div className="App">
                <SwaggerUI url="https://localhost:44460/swagger/v1/swagger.json" />
            </div>
        );
    }
}
