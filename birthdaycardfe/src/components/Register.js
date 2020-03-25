import React from 'react';


export default class Register extends React.Component{

    constructor(){
        super()
        this.register = this.register.bind(this)
    }

    register(e){
        this.props.auth.register(this.username.value,this.email.value, this.password.value, this.confirmPassword)

        this.username.value         = ""
        this.email.value            = ""  
        this.password.value         = "" 
        this.confirmPassword.value  = ""
    }

    render() {
        return (          
            <div>
                <table>
                <thead>
                <tr>
                    <th />
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td> Username: </td>
                    <td> <input type='text' ref={(usernameInput)=> this.username = usernameInput}/> </td>
                </tr>
                <tr>
                    <td> Email: </td>
                    <td> <input type='text' ref={(emailInput)=> this.email = emailInput}/> </td>
                </tr>
                <tr>
                    <td>Password: </td>
                    <td> <input type='text' ref={(passwordInput)=> this.password = passwordInput}/></td>
                </tr>
                <tr>
                    <td>Confirm Password: </td>
                    <td> <input type='text' ref={(confirmPasswordInput)=> this.confirmPassword = confirmPasswordInput}/></td>
                </tr>
                <tr><td><button onClick={this.login}>Register</button></td><td></td></tr>
                </tbody>
                </table>
                <p>{this.props.auth.loginMessage}</p>
            </div>     
        )
    }

}
