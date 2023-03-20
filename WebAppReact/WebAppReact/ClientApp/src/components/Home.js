import React, { Component } from 'react';
import {Department} from "./Department";
import {Employee} from "./Employee";
import {BrowserRouter, Route, Routes, NavLink, Router} from "react-router-dom";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
       <h1> Home</h1>
    );
  }
}
