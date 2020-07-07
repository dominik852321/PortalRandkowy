import { Photo } from './photo';

export interface User {
    //Podstawowe informacje
    userid: number;
    userName: string;
    gender: string;
    age: number;
    zodiacSign: string;
    created: string;
    lastActive: string;
    city: string;
    country: string;
    //Informacje o użytkowniku
    growth: number;
    colorEye: string;
    weight: number;
    martialStatus: string;
    colorSkin: string;
    education: string;
    profession: string;
    langueches?: any;
    children: string;
    //O mnie
    motto: string;
    description: string;
    personality: string;
    lookingFor: string;
    //Pasje zainteresowania
    interest: string;
    freeTime: string;
    sport: string;
    movies: string;
    music: string;
    //Preferencje
    iLike: string;
    iNotLike: string;
    makesMeLaugh: string;
    itFeelsBestIn: string;
    friendsWouldDescribeMe: string;
    //Zdjęcia
    photos: Photo[];
    photoUrl: string;
}
