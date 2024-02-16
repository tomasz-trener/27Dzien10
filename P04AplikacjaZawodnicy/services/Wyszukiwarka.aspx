<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wyszukiwarka.aspx.cs" Inherits="P04AplikacjaZawodnicy.services.Wyszukiwarka" %>


<div class="card">
    <div class="card-header">
        <h4 class="card-title">Zawodnicy</h4>
    </div>

    <div style="margin-left: 15px">
        <a href="SzczegolyZawodnikaGlowny.aspx">Stworz nowy rekord</a>
    </div>


    <div class="card-body">
        <div class="table-responsive">
            <div id="dvKontenerTabelki">
                <table class="table">
                    <thead class=" text-primary">
                        <th>Nazwa
                        </th>
                        <th>Kraj
                        </th>
                        <th>Data urodzenia
                        </th>
                        <th>Wzrost
                        </th>
                        <th>Waga
                        </th>
                        <th>Akcje</th>
                    </thead>
                    <tbody>

                        <%
                            foreach (var z in Zawodnicy)
                            { %>

                        <tr>
                            <td><a href="SzczegolyZawodnikaGlowny.aspx?id=<%= z.Id_zawodnika %>"><%= z.ImieNazwisko %></a></td>
                            <td><%= z.Kraj %></td>
                            <td><%= z.DataUrodzenia?.ToString("dd-MM-yyyy") %></td>
                            <td><%= z.Wzrost %></td>
                            <td><%= z.Waga %></td>
                            <td><a class="btn btn-danger" href="TabelaZawodnikowGlowny.aspx?idUsuwanego=<%= z.Id_zawodnika %>">X</a></td>
                        </tr>

                        <%   }
                        %>
                    </tbody>
                </table>
            </div>

        </div>
    </div>
</div>
