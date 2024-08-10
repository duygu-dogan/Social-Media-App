import React from 'react'
import './home.scss'
import { fileIcon } from './icons'

const Home = () => {
    return (
        <div>
            <div className='posting-main content-center'>
                <form>
                    <div className='flex mx-10 gap-5 post-main-text pb-8 my-4'>
                        <img className='rounded-full w-12 h-12' alt='user' src='https://pbs.twimg.com/profile_images/1555909380117336069/MieF_3XY_bigger.jpg' />
                        <textarea type="text" className='bg-transparent text-xl focus:outline-none' placeholder="What do you think?"
                            rows="1" onInput={(e) => {
                                e.target.style.height = 'auto';
                                e.target.style.height = e.target.scrollHeight + 'px';
                            }} />
                    </div>

                    <div className='flex justify-between mx-10'>
                        <div>
                            <button className='h-10'>{fileIcon} </button>
                            <input accept="image/jpeg,image/png,image/webp,image/gif,video/mp4,video/quicktime" multiple="" type="file" tabindex="-1" hidden />
                        </div>
                        <button className='text-md w-fit px-4 font-semibold text-white no-underline bg-sky-500 rounded-full hover:bg-sky-600'>Submit</button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default Home