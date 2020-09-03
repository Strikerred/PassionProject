import React from 'react'
import { Link } from 'react-router-dom'

export default class Navbar extends React.Component {

    constructor(){
        super()
        this.logOut = this.logOut.bind(this)
    }

    logOut(e){
        this.setState({role: false})
        this.props.auth.logOut()
    }

    render() {   
        return (
            <nav className="navbar">
                <div className="navbar-menu">
                    <div className="navbar-start">
                        <Link to="/" className="navbar-item">Home</Link>
                        {!this.props.auth.sessionStorage && <Link className="navbar-item mx-2" to="/login" >Log in</Link>}  
                        {!this.props.auth.sessionStorage && <Link className="navbar-item" to="/register" >Register</Link>}          
                        {this.props.auth.sessionStorage && <Link className="navbar-item ml-2" to="/login" onClick = {this.logOut} >Log out</Link>}                        
                    </div>
                    <div className="navbar-end">
                        <div className="navbar-item">
                            {this.props.auth.sessionStorage && (<p>Welcome! {this.props.auth.userName}</p>)}                       
                        </div>
                    </div>
                </div>
            </nav>
        )
    }
}