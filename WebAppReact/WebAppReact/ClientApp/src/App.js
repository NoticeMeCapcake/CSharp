import React, { Component } from 'react';
import {BrowserRouter, NavLink, Route, Routes} from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import {variables} from "./Variables";
import './custom.css';
import {Department} from "./components/Department";
import {Employee} from "./components/Employee";

export default class App extends Component {
  static displayName = App.name;
  render() {
    return (
        <BrowserRouter>
            <div className="container-fluid">
                <h3 className="App display3">Home Page</h3>
                <nav className="navbar navbar-expand-sm bg-light navbar-dark">
                    <ul className="navbar-nav">
                        <li className="nav-item- m-1">
                            <NavLink className="btn btn-primary mx-1" to="/department">Department</NavLink>
                            <NavLink className="btn btn-primary mx-1" to="/employee">Employee</NavLink>
                            <NavLink className="btn btn-primary mx-1" to="/home">Home</NavLink>
                        </li>
                    </ul>
                </nav>

                <Routes>
                    <Route path="/" element={<Department />} />
                    <Route path="/department" element={<Department />} />
                    <Route path="/employee" element={<Employee />} />
                </Routes>

            </div>
        </BrowserRouter>
      // <Layout>
      //   <Routes>
      //     {AppRoutes.map((route, index) => {
      //       const { element, ...rest } = route;
      //       return <Route key={index} {...rest} element={element} />;
      //     })}
      //       <Route path="*" element={<h1>Not found</h1>} />
      //   </Routes>
      // </Layout>
        
    );
  }
}
