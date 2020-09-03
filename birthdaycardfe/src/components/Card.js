import React from 'react'
import Moment from 'moment'

const API_INVOKE_URL='https://localhost:44363/api/card';

export default class Card extends React.Component{
    constructor(){
        super();
        this.state = {
            card: {},
        }
    }

    componentDidMount(){
        this.getCard();
    }

    async getCard(){     
            let token   = sessionStorage.auth_token       
            const URL = API_INVOKE_URL+`/${this.props.match.params.id}`;
        await fetch(URL, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
            .then(response => response.json())
            // Data retrieved.
            .then((data) => {           
                this.setState({ card: data });     
            })
            // Data not retrieved.
            .catch(function (error) {
                console.log(error);
            })
    }

    submitInfo(e){
        this.props.auth.submitInfo(this.firstName.value, this.lastName.value, this.age.value)

        this.firstName.value = ""
        this.lastName.value = ""
        this.age.value = ""
    }


    render(){
        return (
            <div className="row justify-content-center">
                <div className="card text-white bg-dark m-3 p-2 col-xs-12" key={this.state.card.templateId}>
                    <img className="card-img-top" src={this.state.card.templateUrl} alt="Card image cap" />
                    <div className="card-body">
                        <table>
                            <thead>
                            <tr>
                                <th />
                            </tr>
                            </thead>
                            <tbody>
                            <tr>
                                <td> First Name: </td>
                                <td> <input type='text' id ="firstName" ref={(firstNameInput)=> this.firstName = firstNameInput}/>  </td>
                                <td> Last Name:  </td>
                                <td> <input type='text' id ="lastName" ref={(lastNameInput)=> this.lastName = lastNameInput}/></td>
                                <td> Turning: </td>
                                <td> <input type='text' id ="age" ref={(ageInput)=> this.age = ageInput}/> </td>
                            </tr>                            
                            <tr><td><button className="mt-3" onClick={this.submitInfo}>Next</button></td><td></td></tr>
                            </tbody>
                        </table>              
                    </div>
                </div> 
            </div>
        )
    }
}