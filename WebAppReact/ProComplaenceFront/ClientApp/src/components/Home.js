import React, { Component } from 'react';
import {variables} from "../Variables";
import "bootstrap/js/src/modal"
// import {getjQuery} from "bootstrap/js/src/util";
// import "jquery/dist/jquery.min.js";
import $ from "jquery";


export class Home extends Component {
  static displayName = Home.name;
  fileUpload = (e) => {
    let reader = new FileReader();
    let file = e.target.files[0];
    reader.onloadend = () => {
      this.setState({
        UserFile: reader.result
      });
      console.log(JSON.stringify({UserFile: reader.result}));
    }
    reader.readAsBinaryString(file);
  }

  constructor(props) {
    super(props);
    this.state = {
      FileList: [],
      modalTitle: "Add file",
      FileName: "",
      FilePath: "",
      LastDate: null,
      FileId: 0,
      UserFile: null,
      FileNameFilter: "",
      FilesWithoutFilter: [],
      
    }
  }

  filterFiles() {
    let file_list = this.state.FilesWithoutFilter;
    // if (this.state.DepartmentIdFilter !== "") {
    //   file_list = file_list.filter(department => department.DepartmentId.toString() === this.state.DepartmentIdFilter);
    // }
    if (this.state.FileNameFilter !== "") {
      file_list = file_list.filter(department => department.DepartmentName.toLowerCase().includes(this.state.DepartmentNameFilter.toLowerCase()));
    }
    this.setState({ FileList: file_list });
  }

  refreshFiles() {
    console.log("GET")
    fetch(variables.API_URL + '/api/fileupload')
        .then(response => response.json())
        .then(data => {this.setState({ FilesWithoutFilter: data });
          this.filterFiles();});

  }

  componentDidMount() {
    this.refreshFiles();
  }

  addClick = () => {
    this.setState({
      modalTitle: "Add File",
    });
  }

  sendMessage = (method, id="") => {
    fetch(variables.API_URL + '/api/fileupload' + (id.length?"/"+id:id), {
      method: method,
      headers: {
        'Accept': 'application/json',
        // 'Content-Type': 'multipart/form-data'
      },
      body: JSON.stringify({
        UserFile: this.UserFile
      })})
        .then(response => response.json())
        .then(data => {
          this.setState({
            modalTitle: "",
            DepartmentName: "",
            DepartmentId: 0,
          });
          this.refreshFiles();
          this.filterFiles();
        }, (error) => {
          alert("Failed:" + error);
        });
  }
  
  
  
  // createClick = () => {
  //   $.ajax({
  //     type: "POST",
  //     url: variables.API_URL + '/api/fileupload',
  //     contentType: false,
  //     processData: false,
  //     data: data,
  //     success: function (result) {
  //       alert(result);
  //     },
  //     error: function (xhr, status, p3) {
  //       alert(xhr.responseText);
  //     }
  //   });
  //   // const form = document.getElementById("fileForm")
    // const output = document.querySelector("#output");
    // const formData = new FormData(form);
    // const request = new XMLHttpRequest();
    // request.open("POST", variables.API_URL + "/api/fileupload", true);
    // request.onload = () => {
    //   // if (request.status === 200) {
    //     console.log(request.responseText);
    //   // }
    // };
    // request.send(formData);
  // }
    // let filePath = this.state.FilePath;
    // this.sendMessage("POST");


  // sendForm = () => { $('#fileForm').on('submit', function(e) {
  //   e.preventDefault();
  //   let formData = new FormData();
  //   formData.append('UserFile', this.UserFile);
  //   formData.append('FileName', this.state.FileName);
  //   formData.append('FilePath', this.state.FilePath);
  //   formData.append('LastDate', this.state.LastDate);
  //   formData.append('FileId', this.state.FileId);
  //   var file = document.getElementById('uploadFile').files[0];
  //   $.ajax({
  //     type: "POST",
  //     url: variables.API_URL + '/api/fileupload',
  //     contentType: false,
  //     processData: false,
  //     data: formData,
  //     success: function (result) {
  //       alert(result);
  //     },
  //     error: function (xhr, status, p3) {
  //       alert(xhr.responseText);
  //     }
  //   })
  // }
  // }
  
  
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
    const { FileList, modalTitle, FileName, FileId, LastDate } = this.state;
    
    return (
        <div>
          <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
          <script src="/js/main.js"></script>
          <button type="button" className="btn btn-primary m-2 float-end"
                  data-bs-toggle="modal"
                  data-bs-target="#FileModal"
                  onClick={()=> this.addClick()}>
            Add File
          </button>
          <table className="table table-striped">
            <thead>
            <tr>
              <th>
                <div className="d-flex flex-row">
                  <input className="form-control my-2 me-2"
                         onChange={(e) => {this.state.FileNameFilter = e.target.value;
                           this.filterFiles();}}
                         placeholder="Filter"/>
                  {this.renderSortingBtn("FileName", true)}
                  {this.renderSortingBtn("FileName", false)}
                </div>
                Name
              </th>
              <th>
                Date
              </th>
              <th className="text-end">
                Options
              </th>
            </tr>
            </thead>
            <tbody>
            {FileList.length !== 0 ? FileList.map((file) => {
              return (
                  <tr key={file.FileId}>
                    <td>
                      {file.FileName}
                    </td>
                    <td>
                      {file.LastDate}
                    </td>
                    <td>
                      <button type="button" className="btn btn-light mx-1 float-end"
                              onClick={()=> this.deleteClick(file.FileId)}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                             fill="currentColor" className="bi bi-trash3" viewBox="0 0 16 16">
                          <path
                              d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z"/>
                        </svg>
                      </button>
                    </td>
                    
                  </tr>
              );
            }) : <div className="display-3">Пусто</div>}
            </tbody>
          </table>
          <div className="modal fade" id="FileModal" tabIndex="-1" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="exampleModalLabel">{modalTitle}</h5>
                  <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                  <form id="fileForm">
                    <div className="input-group mb-3">
                      <span className="input-group-text"> File</span>
                      <input type="file" id="uploadFile" accept="text/csv" className="form-control" onChange={this.fileUpload} />
                    </div>
                    <button type="button" className="btn btn-primary float-start" data-bs-dismiss="modal"
                             onClick={function () {
                               // $('#fileForm').on('submit', function(e) {
                                 // e.preventDefault();
                                 console.log("AJAX")
                                 let formData = new FormData();
                                 // formData.append('FileName', this.state.FileName);
                                 // formData.append('FilePath', this.state.FilePath);
                                 // formData.append('LastDate', this.state.LastDate);
                                 // formData.append('FileId', this.state.FileId);
                                 let file = $('#uploadFile').prop('files')[0];
                                 formData.append('file', file);
                                 $.ajax({
                                   type: "POST",
                                   url: variables.API_URL + '/api/fileupload',
                                   contentType: false,
                                   processData: false,
                                   cache: false,
                                   data: formData})
                                 // .fail(function (xhr, status, p3) {
                                 //   alert(xhr.responseText);
                                 // });
                               // })
                             }}>Create</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        <script>
          
        </script>
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
