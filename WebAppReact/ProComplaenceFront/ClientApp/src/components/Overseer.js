import React, { Component } from 'react';
import {variables} from "../Variables";
import "bootstrap/js/src/modal"
import {useParams, BrowserRouterProps} from "react-router-dom";
import $ from "jquery";



function withParams(Component) {
    return props => <Component {...props} params={useParams()} />;
}


export class Overseer extends Component {
    static displayName = Overseer.name;

    constructor(props) {
        super(props);
        this.state = {
            fileId: window.location.href.split('/')[4], 
            Headers: [],
            UnfilteredData: [],
            DataRows: [],
            DataFilter: [],
        }
    }
    
    componentDidMount() {
        let id  = window.location.href.split('/')[4];
        this.fetchData(id);
        // this.filterFiles();
    }
    
    fetchData(id) {
        fetch(variables.API_URL + '/api/fileread/' + id)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({
                    Headers: data["ExactFile"]["Headers"],
                    // UnfilteredData: data["ExactFile"]["Data"],
                });
                this.state.UnfilteredData = data["ExactFile"]["Data"];
                this.filterFiles();
                // console.log(this.state.Headers);
                // console.log(this.state.Data);
            });
    }

    sortTable(prop, asc) {
        let sortedData = this.state.DataRows.sort((a, b) => {
            const strA = a[prop];
            const strB = b[prop];
            const _a = parseInt(strA);
            const _b = parseInt(strB);            
            if (asc) {
                return (isNaN(_a)? strA : _a) > (isNaN(_b)? strB : _b) ? 1 : (isNaN(_a)? strA : _a) < (isNaN(_b)? strB : _b) ? -1 : 0;
            } else {
                return (isNaN(_b)? strB : _b) > (isNaN(_a)? strA : _a) ? 1 : (isNaN(_b)? strB : _b) < (isNaN(_a)? strA : _a) ? -1 : 0;
            }
        })
        this.setState({ DataRows: sortedData });

    };

    renderSortingBtn (prop, asc) {
        return (
            <div>
                <button type="button" className="btn btn-light m-1 mt-2 p-0" style={{height: "17px"}}
                        onClick={() => this.sortTable(prop, asc)}>
                    {asc ?
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" style={{marginTop: "-13px"}} fill="currentColor"
                             className="bi bi-arrow-down-square " viewBox="0 0 16 16">
                            <path fillRule="evenodd"
                                  d="M15 2a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2zM0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2zm8.5 2.5a.5.5 0 0 0-1 0v5.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V4.5z"/>
                        </svg> :
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" style={{marginTop: "-13px"}} fill="currentColor"
                             className="bi bi-arrow-up-square" viewBox="0 0 16 16">
                            <path fillRule="evenodd"
                                  d="M15 2a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2zM0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2zm8.5 9.5a.5.5 0 0 1-1 0V5.707L5.354 7.854a.5.5 0 1 1-.708-.708l3-3a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 5.707V11.5z"/>
                        </svg>
                    }
    
                </button>
            </div>
        )

    }

    filterFiles() {
        let data_list = this.state.UnfilteredData;
        data_list = data_list.filter(row => {
            return Object.keys(row).every(key => {
                let filterValue = this.state.DataFilter[key];
                if (filterValue !== "" && filterValue !== undefined && filterValue !== null) {
                    return row[key].toString().toLowerCase().includes(filterValue.toString().toLowerCase());
                }
                return true;
            });
        });
        // console.log(data_list);
        // let data_list1 = this.state.DataRows;
        // for (const [index1, row] of this.state.UnfilteredData.entries()) {
        //     console.log("Filter1: " + index1)
        //     console.log(row);
        //     for (const [index, cell] of row.entries()) {
        //         let filterValue = this.state.DataFilter[index];
        //         console.log("Filter: " + filterValue)
        //         if (filterValue !== "" && filterValue !== undefined && filterValue !== null) {
        //            
        //             if (!cell.toLowerCase().includes(filterValue.toLowerCase())) {
        //                 data_list.splice(index1, 1);
        //                 break;
        //             }
        //         }
        //     }
        // }
        // this.state.FilesList = file_list;
        this.setState({ DataRows: data_list });
    }

    render() {
        const {Headers, DataRows} = this.state;
        console.log(Headers);
        console.log( window.location.href.split('/')[4]);
        return (
          <div>
              <h1>
                  Overseer
              </h1>
              <table className="table table-striped">
                  <thead>
                      <tr>
                          {Headers.map((header, index) => {
                              return (<th key={index}>
                                  <div className="d-flex flex-row">
                                      <input className="form-control my-2 me-1 p-0" style={{minWidth: "50px"}}
                                             onChange={(e) => {
                                                 console.log(index + " : " + e.target.value);
                                                 this.state.DataFilter[index] = e.target.value;
                                                 console.log(index + " : " + this.state.DataFilter[index]);
                                                 this.filterFiles();
                                             }}
                                             placeholder="Filter"/>
                                      {this.renderSortingBtn(index, true)}
                                      {this.renderSortingBtn(index, false)}
                                  </div>
                                  {header + index}
                              </th>);
                          })}
                      </tr>
                  </thead>
                  <tbody>
                      {DataRows.map((row, index1) => {
                          return (
                              <tr key={index1}>
                                  {row.map((cell, index2) => {
                                      return (<td key={index2}>{cell}</td>);
                                  })}
                              </tr>
                          );
                      })}
                  </tbody>
              </table>
          </div>
            
        );
    }
}