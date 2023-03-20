import React, { Component } from 'react';


export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
       <div className="container-fluid">
         <div className="row row-cols-2 col-11 justify-content-around d-flex">
           <div className="col-md-5 col-11 border-dark text-light" style={{background: "#868686", borderRadius: "15px"}}>
               <h2 className="display-4">This is the app for managing your departments!</h2>
           </div>
            <div className="col-md-5 col-11 border-dark text-light" style={{background: "#868686", borderRadius: "15px"}}>
                <h2 className="display-4">It's using other web api to modify data of DB.</h2>
            </div>
             <div className="col-10 text-light m-5" style={{background: "#868686", borderRadius: "15px"}}>
                 <h2 className="display-4">
                     Quite simple and easy to use)
                 </h2>
             </div>
         </div>
       </div>
    );
  }
}
