import React, { Component } from 'react';
import {variables} from "../Variables";
import "bootstrap/js/src/modal"

export class Employee extends Component {
    static displayName = Employee.name;

    constructor(props) {
        super(props);
        this.state = {
            departments: [],
            employees: [],
            modalTitle: "",
            DepartmentName: "",
            EmployeeId: 0,
            EmployeeName: "",
            DepartmentId: 1,
            DateOfJoining: "",
            Photo: null,
            Age: 0,
        }
    }

    refresh() {
        console.log("GET")
        fetch(variables.API_URL + '/department')
            .then(response => response.json())
            .then(data => this.setState({ departments: data }));
        fetch(variables.API_URL + '/employee')
            .then(response => response.json())
            .then(data => this.setState({ employees: data }));
    }

    componentDidMount() {
        this.refresh();
    }

    addClick = () => {
        this.setState({
            modalTitle: "Add Employee",
            DepartmentName: "",
            DepartmentId: 1,
            EmployeeId: 0,
            EmployeeName: "",
            DateOfJoining: "",
            Photo: null,
            Age: 0
        });
    }

    editClick = (emp) => {
        this.setState({
            modalTitle: "Edit Department",
            DepartmentId: emp.DepartmentId,
            EmployeeName: emp.EmployeeName,
            DateOfJoining: emp.DateOfJoining,
            Photo: emp.Photo,
            Age: emp.Age,
        });
    }

    sendMessage = (method, id="") => {
        console.log(JSON.stringify({
            EmployeeName: this.state.EmployeeName,
            Age: this.state.Age,
            DepartmentId: this.state.DepartmentId,
            DateOfJoining: this.state.DateOfJoining,
            Photo: this.state.Photo
        }));
        fetch(variables.API_URL + '/employee' + (id.length?"/"+id:id), {
            method: method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                EmployeeName: this.state.EmployeeName,
                DepartmentId: this.state.DepartmentId,
                Age: this.state.Age,
                DateOfJoining: this.state.DateOfJoining,
                Photo: this.state.Photo
            })
        })
            .then(response => response.json())
            .then(data => {
                this.refresh();
                this.setState({
                    modalTitle: "",
                    DepartmentName: "",
                    EmployeeId: 0,
                    EmployeeName: "",
                    DepartmentId: 1,
                    DateOfJoining: "",
                    Photo: null,
                    Age: 0,
                })
            }, (error) => {
                alert("Failed:" + error);
            });
    }
    createClick = () => {
        console.log("POST")
        this.sendMessage("POST");
    }
    updateClick = () => {
        console.log("PUT")
        this.sendMessage("PUT");
    }

    deleteClick = (id) => {
        if(window.confirm("Are you sure you want to delete this employee?")) {
            console.log("DELETE")
            this.sendMessage("DELETE", id.toString());
        }
    }
    
    imgUpload = (e) => {
        console.log("imgUploasd")
        let reader = new FileReader();
        let file = e.target.files[0];
        // createObjectURL;
        reader.onloadend = () => {
            this.setState({
                Photo: reader.result.slice(reader.result.indexOf(',')+1)
            });
            console.log(JSON.stringify({Photo: reader.result.slice(reader.result.indexOf(',')+1)}));
            console.log(JSON.stringify({Photo: reader.result}));
        }
        // reader.readAsDataURL(file);
        reader.readAsDataURL(file);
    }

    render() {
        const { departments,
            employees, modalTitle, DepartmentName, DepartmentId,
            EmployeeId, EmployeeName, DateOfJoining, Photo, Age } = this.state;
        return (
            <div>
                <button type="button" className="btn btn-primary m-2 float-end"
                        data-bs-toggle="modal"
                        data-bs-target="#DepartmentModal"
                        onClick={()=> this.addClick()}>
                    Add employee
                </button>
                <table className="table table-striped">
                    <thead>
                    <tr>
                        <th>
                            Id
                        </th>
                        <th>
                            Name
                        </th>
                        <th>
                            Department
                        </th>
                        <th>
                            DOJ
                        </th>
                        <th>
                            Age
                        </th>
                        <th className="float-end">
                            Options
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {employees.map((emp) => {
                        return (
                            <tr key={emp.EmployeeId}>
                                <td>
                                    {emp.EmployeeId}
                                </td>
                                <td>
                                    {emp.EmployeeName}
                                </td>
                                <td>
                                    {emp.DepartmentName}
                                </td>
                                <td>
                                    {emp.DateOfJoining.slice(0, 10)}
                                </td>
                                <td>
                                    {emp.Age}
                                </td>
                                <td>
                                    <button type="button" className="btn btn-light mx-1 float-end"
                                            data-bs-toggle="modal"
                                            data-bs-target="#DepartmentModal"
                                            onClick={()=> this.editClick(emp)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                             fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path
                                                d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                            <path fillRule="evenodd"
                                                  d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light mx-1 float-end"
                                            onClick={()=> this.deleteClick(emp.EmployeeId)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                             fill="currentColor" className="bi bi-trash3" viewBox="0 0 16 16">
                                            <path
                                                d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z"/>
                                        </svg>
                                    </button>
                                </td>
                            </tr>
                        );
                    })}
                    </tbody>
                </table>
                <div className="modal fade modal-lg" id="DepartmentModal" tabIndex="-1" aria-hidden="true">
                    
                    <div className="modal-dialog modal-dialog-centered" >
                        <div className="modal-content" >
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLabel">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">
                                    <div className="p-2 w-50 bd-highlight">
                                        <div className="input-group mb-3">
                                            <span className="input-group-text"> Name</span>
                                            <input type="text" className="form-control" value={EmployeeName} onChange={(e) => this.setState({ EmployeeName: e.target.value })} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text"> Department</span>
                                            <select className="form-select" id="depSelect"
                                                    onChange={(e) => {
                                                        this.setState({DepartmentId: e.target.value});
                                                    }}
                                            >
                                                {departments.map((dep) => {
                                                    return (
                                                        <option key={dep.DepartmentId} value={dep.DepartmentId}>
                                                            {dep.DepartmentName}
                                                        </option>
                                                    );
                                                })}
                                            </select>
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text"> DOJ</span>
                                            <input type="date" className="form-control" value={DateOfJoining} onChange={(e) => this.setState({ DateOfJoining: e.target.value })} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text"> Age</span>
                                            <input type="text" className="form-control" value={Age} onChange={(e) => this.setState({ Age: e.target.value })} />
                                        </div>
                                    </div>
                                    <div className="p-2 w-50 bd-highlight">
                                        <img width="250px" height="250px" src={Photo === null ? null : ("data:image/jpeg;base64,"+Photo)}/>
                                        <input className="m-2" type="file" onChange={this.imgUpload}/>
                                    </div>
                                </div>
                                {EmployeeId == 0?
                                    <button type="button" className="btn btn-primary float-start" data-bs-dismiss="modal"
                                            onClick={() => this.createClick()} >Create</button>
                                    :null}
                                {EmployeeId != 0?
                                    <button type="button" className="btn btn-primary float-start" data-bs-dismiss="modal"
                                            onClick={() => this.updateClick()} >Update</button>
                                    :null}
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}
