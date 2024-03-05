import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { Provincia } from "src/interfaces/provincia";

@Injectable()
export class ProvinciaProvider {

    urlApi: string = environment.urlApi

    constructor(private http: HttpClient) {
    }

    ObtenerTodas(): Observable<Provincia[]> {
        const url = this.urlApi + 'api/Provincia/ObtenerTodas';
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Provincia[]>(url, { 'headers': headers })
    }
}