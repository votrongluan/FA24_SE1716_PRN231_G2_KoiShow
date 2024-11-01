using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShow.Data.Migrations
{
    /// <inheritdoc />
    public partial class addordertypepayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2320), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2321) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2330), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2331) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2336), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2338) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2342), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2344) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2349), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2544), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2545) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2553), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2554) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2559), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2565), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2566) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2572), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2573) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2580), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2581) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2586), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2587) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2593), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2594) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2599), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3086), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3087) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3094), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3095) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3101), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3102) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3107), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3108) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3113), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3114) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3120), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3121) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3126), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3127) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3132), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3133) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3138), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3139) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2670), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2671) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2680), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2681) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2688), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2689) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2695), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2696) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2703), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2704) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2712), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2713) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2720), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2721) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2727), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2728) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2735), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2736) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3191), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3192), null, new DateTime(2024, 10, 13, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3183) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3201), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3202), null, new DateTime(2024, 10, 15, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3198) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3208), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3209), null, new DateTime(2024, 10, 17, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3216), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3217), null, new DateTime(2024, 10, 19, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3213) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3223), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3224), null, new DateTime(2024, 10, 21, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3231), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3232), null, new DateTime(2024, 10, 22, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3228) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3238), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3239), null, new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3235) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3245), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3246), null, new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3242) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "OrderType", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3252), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3253), null, new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(3249) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2908), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2909) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2917), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2918) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2934), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2935) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2940), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2941) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2946), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2947) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2953), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2954) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2959), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2965), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2966) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2972), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2973) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2793), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2794) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2802), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2803) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2812), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2813) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2818), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2819) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2825), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2826) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2833), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2834) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2839), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2846), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2847) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2853), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2854) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2444), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2445) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2450), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2451) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2455), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2456) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2459), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2463), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2464) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2468), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2469) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2472), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2473) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2477), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2478) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2481), new DateTime(2024, 10, 23, 7, 48, 31, 77, DateTimeKind.Local).AddTicks(2482) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4160), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4162) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4168), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4169) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4172), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4173) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4176), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4176) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4180), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4277), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4278) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4283), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4283) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4287), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4287) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4291), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4292) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4295), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4295) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4299), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4303), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4303) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4306), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4307) });

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4310), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4555), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4555) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4559), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4560) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4563), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4563) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4566), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4567) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4570), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4570) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4574), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4574) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4577), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4578) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4580), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4582) });

            migrationBuilder.UpdateData(
                table: "ContestResults",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4584), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4585) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4343), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4344) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4349), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4354), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4355) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4359), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4359) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4363), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4364) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4370), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4371) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4375), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4375) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4379), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4379) });

            migrationBuilder.UpdateData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4383), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4384) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4611), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4612), new DateTime(2024, 10, 8, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4605) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4617), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4618), new DateTime(2024, 10, 10, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4621), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4622), new DateTime(2024, 10, 12, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4625), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4626), new DateTime(2024, 10, 14, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4629), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4630), new DateTime(2024, 10, 16, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4628) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4634), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4635), new DateTime(2024, 10, 17, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4632) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4638), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4639), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4636) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4642), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4642), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "PaymentDate" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4646), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4646), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4644) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4474), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4474) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4479), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4479) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4487), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4488) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4491), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4491) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4494), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4495) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4498), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4498) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4501), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4502) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4505), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4505) });

            migrationBuilder.UpdateData(
                table: "Points",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4508), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4508) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4411), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4411) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4418), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4418) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4422), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4422) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4425), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4426) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4430), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4435), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4435) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4439), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4439) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4442), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4443) });

            migrationBuilder.UpdateData(
                table: "RegisterForms",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4446), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4446) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4216), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4216) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4220), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4222), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4222) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4226), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4227) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4229), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4229) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4232), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4232) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4234), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4234) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4236), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4236) });

            migrationBuilder.UpdateData(
                table: "Varieties",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4238), new DateTime(2024, 10, 18, 15, 31, 39, 153, DateTimeKind.Local).AddTicks(4238) });
        }
    }
}
