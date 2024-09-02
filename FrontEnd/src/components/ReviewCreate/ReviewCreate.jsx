import { useEffect, useState } from 'react';
import axios from 'axios';

import globalStyles from '../../Global.module.css'
import styles from './ReviewCreate.module.css';

export default function ReviewCreate(props) {

    //Members below do not need re-render on change
    let rating = 0;
    let description = '';

    const submitReview = () => {
        const today = new Date();

        let review = {
            firstKey: props.userId,
            secondKey: props.bookId,
            date_submitted: `${today.getFullYear()}-${today.getMonth()}-${today.getDay()}`,
            rating: rating,
            description: description
        };
    }

    return (
        <>
        <div id={styles.DimBackground}></div>
        <form id={`${styles.ReviewForm}`} className={`${globalStyles.popupDialogue}`} style={{padding:'50px'}}>
            <div>
                <select name='rating'>
                    <option value='0'>0</option>
                    <option value='1'>1</option>
                    <option value='2'>2</option>
                    <option value='3'>3</option>
                    <option value='4'>4</option>
                    <option value='5'>5</option>
                    <option value='6'>6</option>
                    <option value='7'>7</option>
                    <option value='8'>8</option>
                    <option value='9'>9</option>
                    <option value='10'>10</option>
                </select>
            </div>
            <div style={{width:'100%', height:'50%', marginTop:'20px'}}>
                <input id={`${styles.DescriptionBox}`} className={`${globalStyles.fillContainer}`} type='text'/>
            </div>
        </form>
        </>
    )
}