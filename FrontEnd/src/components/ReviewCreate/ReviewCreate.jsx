import { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

import { UserContext } from '../ProfileShelf';

import globalStyles from '../../Global.module.css'
import styles from './ReviewCreate.module.css';

export default function ReviewCreate(props) {

    const user = useContext(UserContext);

    //Members below do not need re-render on change
    const MAXRATING = 10;
    let rating = 0;
    let description = '';

    const formatNumber = (num) => {return (num < 10) ? ("0" + num) : num;}

    const submitReview = () => {
        const today = new Date();
        let year = today.getFullYear();
        let month = formatNumber(today.getMonth() + 1);
        let day = formatNumber(today.getDay() + 1);

        let review = {
            firstKey: user,
            secondKey: props.bookId,
            description: description,
            rating: rating,
            date_submitted: `${year}-${month}-${day}`
        };
          

        axios.post('https://localhost:7213/api/Review/AddReview', review)
        .catch(err => console.error(err));

        props.popupBool();
    }

    const createOptions = () => {
        let options = [];

        for(let i = 0;i <= MAXRATING;i++)
        {
            options.push((<option key={i} value={i}>{i}</option>));
        }

        return options;
    }

    const setRating = (event) => { rating = event.target.value; }
    const setDescription = (event) => { description = event.target.value; }

    return (
        <>
        <div id={styles.DimBackground}></div>
        <form id={`${styles.ReviewForm}`} className={`${globalStyles.popupDialogue}`} style={{padding:'10px'}}>
            <div style={{fontSize:'30px', fontWeight:'bold'}}>Create Review</div>
            <select name='rating' onChange={setRating} style={{background:'rgb(220, 220, 200)', color:'black'}}>
                {createOptions()}
            </select>
            <textarea id={`${styles.DescriptionBox}`} className={`${globalStyles.fillContainer} ${globalStyles.darkText}`} style={{width:'100%', height:'50%', marginTop:'10px'}} onChange={setDescription}/>
            <div className={`${globalStyles.interactible}`} style={{float:'left', width:'fit-content', marginTop:'5px'}} onClick={props.popupBool}>Return</div>
            <div className={`${globalStyles.interactible}`} style={{float:'right', width:'fit-content', marginTop:'5px'}} onClick={() => submitReview()}>Submit Review</div>
        </form>
        </>
    )
}