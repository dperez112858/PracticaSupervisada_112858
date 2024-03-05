import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { Cobranza } from "src/interfaces/cobranza";
import { Intervalo } from "src/interfaces/intervalo";

@Injectable()
export class CobranzaProvider {

    urlApi: string = environment.urlApi
    cobranza = {} as Cobranza;

    constructor(private http: HttpClient) {
    }

    Crear(cobranza: Cobranza): Observable<any> {
        const url = this.urlApi + 'api/Cobranza/Crear';
        const headers = { 'content-type': 'application/json' };
        const body = JSON.stringify(cobranza);
        return this.http.post(url, body, { 'headers': headers });
    }
    ObtenerTodas(): Observable<any> {
        return this.http.get(this.urlApi + 'api/Cobranza/ObtenerTodas');
    }

    ObtenerCobranzasPorMes(intervalo: Intervalo): Observable<any> {
        const url = this.urlApi + 'api/Cobranza/ObtenerCobranzasPorMes';
        const headers = { 'content-type': 'application/json' }
        const body = JSON.stringify(intervalo);
        return this.http.post(url, body, { 'headers': headers });
    }

    ObtenerPorId(id?: any): Observable<Cobranza> {
        const url = this.urlApi + 'api/Cobranza/ObtenerPorId/?id=' + id;
        const headers = { 'content-type': 'application/json' }
        return this.http.get<Cobranza>(url, { 'headers': headers })
    }

    Eliminar(id: string): Observable<any> {
        return this.http.put(this.urlApi + 'api/Cobranza/Eliminar/?id=' + id, null);
    }

    Editar(cobranza: Cobranza): Observable<Cobranza> {
        const url = this.urlApi + 'api/Cobranza/Actualizar';
        return this.http.put<Cobranza>(url, cobranza);
    }
}
