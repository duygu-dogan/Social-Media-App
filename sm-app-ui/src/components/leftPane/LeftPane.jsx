import React from 'react'
import { NavLink } from 'react-router-dom'
import './leftPane.scss'
import { faTwitter } from '@fortawesome/free-brands-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { bookmarksIcon, exploreIcon, homeIcon, listsIcon, messagesIcon, moreIcon, notificationsIcon, profileIcon } from './icons'


function LeftPane() {
    return (
        <>
            <div className='left-pane'>
                <div className='container ml-28 mt-5 min-h-screen'>
                    <header>
                        <FontAwesomeIcon className='text-4xl mb-3' icon={faTwitter} />
                    </header>
                    <nav>
                        <NavLink to='/'>
                            <span>{homeIcon} Home </span>
                        </NavLink>
                        <NavLink to='/explore'>
                            <span> {exploreIcon} Explore </span>
                        </NavLink>
                        <NavLink to='/notifications'>
                            <span> {notificationsIcon} Notifications </span>
                        </NavLink>
                        <NavLink to='/messages'>
                            <span> {messagesIcon} Messages </span>
                        </NavLink>
                        <NavLink to='/bookmarks'>
                            <span> {bookmarksIcon} Bookmarks </span>
                        </NavLink>
                        <NavLink to='/lists'>
                            <span> {listsIcon} Lists </span>
                        </NavLink>
                        <NavLink to='/profile'>
                            <span> {profileIcon} Profile </span>
                        </NavLink>
                        <button className='more-btn'>
                            <span> {moreIcon} More </span>
                        </button>
                    </nav>

                    <button className='tweet-btn'>Tweet</button>

                    <footer className='footer'>
                        <button className='account'>
                            <div className='account-photo'>
                                <img alt='user' src='https://pbs.twimg.com/profile_images/1555909380117336069/MieF_3XY_bigger.jpg' />
                            </div>
                            <div className='account-info'>
                                <strong>Jane Doe</strong>
                                <span>@janedoe</span>
                            </div>
                        </button>
                    </footer>
                </div>
            </div>
        </>
    )
}

export default LeftPane