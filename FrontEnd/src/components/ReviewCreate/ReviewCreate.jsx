import { useContext, useState } from 'react';
import axios from 'axios';

import { UserContext } from '../ProfileShelf';

import globalStyles from '../../Global.module.css'
import styles from './ReviewCreate.module.css';

export default function ReviewCreate(props) {

    const user = useContext(UserContext);

    const [success, setSuccess] = useState(true);

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
            firstKey: 1,
            secondKey: props.bookId,
            description: description,
            rating: rating,
            date_submitted: `${year}-${month}-${day}`
        };
          

        axios.post('https://localhost:7213/api/Review/AddReview', review)
        .then(result => {
            setSuccess(result.success);
        })
        .catch(err => console.error(err));
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
        <form id={`${styles.ReviewForm}`} className={`${globalStyles.popupDialogue}`} style={{padding:'50px'}}>
            <div>
                <select name='rating' onChange={setRating}>
                    {createOptions()}
                </select>
            </div>
            <div style={{width:'100%', height:'50%', marginTop:'20px'}}>
                <input id={`${styles.DescriptionBox}`} className={`${globalStyles.fillContainer}`} onChange={setDescription} type='text'/>
            </div>
            <div className={`${globalStyles.interactible}`} onClick={() => submitReview()}>Submit Review</div>
        </form>
        </>
    )
}