import React, { Component } from 'react';
import {variables} from "../Variables";
import "bootstrap/js/src/modal"

export class Department extends Component {
    static displayName = Department.name;

    constructor(props) {
        super(props);
        this.state = {
            departments: [],
            modalTitle: "",
            DepartmentName: "",
            DepartmentId: 0,
            
            DepartmentIdFilter: "",
            DepartmentNameFilter: "",
            DepartmentsWithoutFilter: [],
        }
    }
    
    filterDepartments() {
        let departments = this.state.DepartmentsWithoutFilter;
        if (this.state.DepartmentIdFilter !== "") {
            departments = departments.filter(department => department.DepartmentId.toString() === this.state.DepartmentIdFilter);
        }
        if (this.state.DepartmentNameFilter !== "") {
            departments = departments.filter(department => department.DepartmentName.toLowerCase().includes(this.state.DepartmentNameFilter.toLowerCase()));
        }
        this.setState({ departments: departments });
    }
    
    refreshDepartments() {
        console.log("GET")
        fetch(variables.API_URL + '/department')
            .then(response => response.json())
            .then(data => {this.setState({ DepartmentsWithoutFilter: data });
                    this.filterDepartments();});
        
    }
    
    componentDidMount() {
        this.refreshDepartments();
    }
    
    addClick = () => {
        this.setState({
            modalTitle: "Add Department",
            DepartmentName: "",
            DepartmentId: 0,
        });
    }
    
    editClick = (dep) => {
        this.setState({
            modalTitle: "Edit Department",
            DepartmentName: dep.DepartmentName,
            DepartmentId: dep.DepartmentId,
        });
    }
    
    sendMessage = (method, id="") => {
        fetch(variables.API_URL + '/department' + (id.length?"/"+id:id), {
            method: method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                DepartmentId: this.state.DepartmentId,
                DepartmentName: this.state.DepartmentName,
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    modalTitle: "",
                    DepartmentName: "",
                    DepartmentId: 0,
                });
                this.refreshDepartments();
                this.filterDepartments();
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
        if(window.confirm("Are you sure you want to delete this department?")) {
            console.log("DELETE")
            this.sendMessage("DELETE", id.toString());
        }
    }

    renderSortingBtn (prop, asc) {
        return (
            <button type="button" className="btn btn-light m-1"
                    onClick={() => this.sortTable(prop, asc)}>
                {asc ?
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                         className="bi bi-arrow-down-square" viewBox="0 0 16 16">
                        <path fillRule="evenodd"
                              d="M15 2a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2zM0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2zm8.5 2.5a.5.5 0 0 0-1 0v5.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V4.5z"/>
                    </svg> :
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                         className="bi bi-arrow-up-square" viewBox="0 0 16 16">
                        <path fillRule="evenodd"
                              d="M15 2a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2zM0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2zm8.5 9.5a.5.5 0 0 1-1 0V5.707L5.354 7.854a.5.5 0 1 1-.708-.708l3-3a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 5.707V11.5z"/>
                    </svg>
                }

            </button>
        )

    }
    
    render() {
        const { departments, modalTitle, DepartmentName, DepartmentId } = this.state;
        return (
            <div>
                <button type="button" className="btn btn-primary m-2 float-end" 
                data-bs-toggle="modal"
                data-bs-target="#DepartmentModal"
                onClick={()=> this.addClick()}>
                    Add department
                </button>
                <table className="table table-striped">
                   <thead>
                   <tr>
                       <th>
                           <div className="d-flex flex-row">
                               <input className="form-control my-2 me-2"
                               onChange={(e) => {this.state.DepartmentIdFilter = e.target.value;
                               this.filterDepartments();}}
                               placeholder="Filter"/>
                               {this.renderSortingBtn("DepartmentId", true)}
                               {this.renderSortingBtn("DepartmentId", false)}
                           </div>
                               
                           Id
                       </th>
                       <th>
                           <div className="d-flex flex-row">
                               <input className="form-control my-2 me-2"
                                      onChange={(e) => {this.state.DepartmentNameFilter = e.target.value;
                                          this.filterDepartments();}}
                                      placeholder="Filter"/>
                               {this.renderSortingBtn("DepartmentName", true)}
                               {this.renderSortingBtn("DepartmentName", false)}
                           </div>
                           DepartmentName
                       </th>
                       <th className="text-end">
                           Options
                       </th>
                   </tr>
                   </thead>
                    <tbody>
                    {departments.map((department) => {
                        return (
                            <tr key={department.DepartmentId}>
                                <td>
                                    {department.DepartmentId}
                                </td>
                                <td>
                                    {department.DepartmentName}
                                </td>
                                <td>
                                    <button type="button" className="btn btn-light mx-1 float-end"
                                            data-bs-toggle="modal"
                                            data-bs-target="#DepartmentModal"
                                            onClick={()=> this.editClick(department)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                             fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path
                                                d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                            <path fillRule="evenodd"
                                                  d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light mx-1 float-end"
                                            onClick={()=> this.deleteClick(department.DepartmentId)}>
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
                <div className="modal fade" id="DepartmentModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLabel">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body">
                                <div className="input-group mb-3">
                                    <span className="input-group-text"> DepartmentName</span>
                                    <input type="text" className="form-control" value={DepartmentName} onChange={(e) => this.setState({ DepartmentName: e.target.value })} />
                                </div>
                                {DepartmentId === 0?
                                    <button type="button" className="btn btn-primary float-start" data-bs-dismiss="modal" 
                                        onClick={() => this.createClick()} >Create</button>
                                    :null}
                                {DepartmentId !== 0?
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

    sortTable(prop, asc) {
        let sortedData = this.state.departments.sort((a, b) => {
            if (asc) {
                return a[prop] > b[prop] ? 1 : a[prop] < b[prop] ? -1 : 0;
            } else {
                return b[prop] > a[prop] ? 1 : b[prop] < a[prop] ? -1 : 0;
            }
        })
        this.setState({ departments: sortedData });
        
    }
}
