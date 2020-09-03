import React from 'react';


export default class Login extends React.Component{

    constructor(){
        super()
        this.login = this.login.bind(this)
    }

    login(e){
        this.props.auth.logIn(this.email.value, this.password.value, false)

        this.email.value    = ""; // Clear input.  
        this.password.value = ""; // Clear input.
    }

    render() {
        return (          
            <div>{!this.props.auth.sessionStorage && (
                <table>
                <thead>
                <tr>
                    <th />
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td> Email: </td>
                    <td> <input type='text' ref={(emailInput)=> this.email = emailInput}/> </td>
                </tr>
                <tr>
                    <td>Password: </td>
                    <td> <input type='text' ref={(passwordInput)=> this.password = passwordInput}/></td>
                </tr>
                <tr><td><button onClick={this.login}>Login</button></td><td></td></tr>
                </tbody>
                </table>)}
                <p>{this.props.auth.loginMessage}</p>
            </div>     
        )
    }

}
