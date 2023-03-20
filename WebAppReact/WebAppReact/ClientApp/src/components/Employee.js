import React, { Component } from 'react';

export class Employee extends Component {
    static displayName = Employee.name;

    render() {
        return (
            <div>
                <h3 className="display3">Employee Page</h3>
            </div>
        );
    }
}
